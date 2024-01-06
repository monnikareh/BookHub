using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Packaging;

namespace BusinessLayer.Services;

using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class BookService : IBookService
{
    private readonly BookHubDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public BookService(BookHubDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }


    public async Task<IEnumerable<BookDetail>> GetBooksAsync(string? bookName, int? genreId,
        string? genreName,
        int? publisherId, string? publisherName, int? authorId, string? authorName)
    {
        var books = _context.Books
            .Include(pg => pg.PrimaryGenre)
            .Include(g => g.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .AsQueryable();

        var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genreName || g.Id == genreId);
        if (genre != null)
        {
            books = books.Where(b => b.Genres.Contains(genre));
        }

        var publisher =
            await _context.Publishers.FirstOrDefaultAsync(p => p.Name == publisherName || p.Id == publisherId);
        if (publisher != null)
        {
            books = books.Where(b => b.Publisher.Id == publisher.Id);
        }

        var author = await _context
            .Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Name == authorName || a.Id == authorId);

        if (author != null)
        {
            books = books.Where(b => b.Authors.Contains(author));
        }

        if (!bookName.IsNullOrEmpty())
        {
            books = books.Where(b => b.Name == bookName);
        }

        var filteredBooks = await books.ToListAsync();
        return filteredBooks.Select(EntityMapper.MapBookToBookDetail);
    }

    public async Task<BookDetail> GetBookByIdAsync(int id)
    {
        var key = $"BookById_{id}";
        if (_memoryCache.TryGetValue(key, out BookDetail? cached) && cached is not null)
        {
            return cached;
        }

        var book = await _context
            .Books
            .Include(pg => pg.PrimaryGenre)
            .Include(g => g.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
        {
            throw new BookNotFoundException($"Book 'ID={id}' could not be found");
        }

        var mapped = EntityMapper.MapBookToBookDetail(book);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
        _memoryCache.Set(key, mapped, cacheEntryOptions);
        return mapped;
    }


    public async Task<BookDetail> CreateBookAsync(BookCreate bookCreate)
    {
        if (bookCreate.Authors.IsNullOrEmpty())
        {
            throw new AuthorsEmptyException("Collection Authors is empty");
        }

        var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Name == bookCreate.Publisher.Name
                                                                           || p.Id == bookCreate.Publisher.Id);
        if (publisher == null)
        {
            throw new PublisherNotFoundException(
                $"Publisher 'Name={bookCreate.Publisher.Name}' <OR> 'ID={bookCreate.Publisher.Id}' could not be found");
        }

        var primaryGenre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == bookCreate.PrimaryGenre.Name
                                                                          || g.Id == bookCreate.PrimaryGenre.Id);

        if (primaryGenre == null)
        {
            throw new GenreNotFoundException(
                $"PrimaryGenre 'Name={bookCreate.PrimaryGenre.Name}' <OR> 'ID={bookCreate.PrimaryGenre.Id}' could not be found");
        }

        var genreNames = bookCreate.Genres.Select(g => g.Name).ToHashSet();
        var genreIds = bookCreate.Genres.Select(g => g.Id).ToHashSet();
        var genres = await _context.Genres.Where(g => genreNames.Contains(g.Name) || genreIds.Contains(g.Id))
            .ToListAsync();

        if (genres.Count != bookCreate.Genres.Count)
        {
            throw new GenreNotFoundException("One or more genres could not be found");
        }

        if (bookCreate.Authors == null)
        {
            throw new AuthorsEmptyException("One or more author could not be found");
        }

        var authorNames = bookCreate.Authors.Select(a => a.Name).ToHashSet();
        var authorIds = bookCreate.Authors.Select(a => a.Id).ToHashSet();
        var authors = await _context.Authors.Where(a => authorNames.Contains(a.Name) || authorIds.Contains(a.Id))
            .ToListAsync();

        if (authors.Count != bookCreate.Authors.Count)
        {
            throw new AuthorNotFoundException("One or more authors could not be found");
        }

        var book = new Book
        {
            Name = bookCreate.Name,
            Authors = authors,
            Genres = genres,
            Publisher = publisher,
            PublisherId = publisher.Id,
            PrimaryGenre = primaryGenre,
            PrimaryGenreId = primaryGenre.Id,
            Price = bookCreate.Price,
            StockInStorage = bookCreate.StockInStorage,
            OverallRating = bookCreate.OverallRating
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return EntityMapper.MapBookToBookDetail(book);
    }

    public async Task<BookDetail> UpdateBookAsync(int id, BookCreate bookUpdate)
    {
        var book = await _context.Books
            .Include(pg => pg.PrimaryGenre)
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            throw new BookNotFoundException($"Book 'ID={id}' could not be found");
        }

        book.Name = bookUpdate.Name;

        if (bookUpdate.Genres.Count > 0)
        {
            var genreNames = bookUpdate.Genres.Select(g => g.Name).ToHashSet();
            var genreIds = bookUpdate.Genres.Select(g => g.Id).ToHashSet();

            var genres = await _context.Genres
                .Where(g => genreNames.Contains(g.Name) || genreIds.Contains(g.Id))
                .ToListAsync();

            if (genres.Count != bookUpdate.Genres.Count)
            {
                throw new GenreNotFoundException("One or more genres could not be found");
            }
            book.Genres.Clear();
            book.Genres.AddRange(genres);
        }

        if (bookUpdate.Publisher.Name != "string")
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p =>
                p.Name == bookUpdate.Publisher.Name || p.Id == bookUpdate.Publisher.Id);
            if (publisher == null)
            {
                throw new PublisherNotFoundException(
                    $"Publisher 'Name={bookUpdate.Publisher.Name}' <OR> 'ID={bookUpdate.Publisher.Id}' could not be found");
            }

            book.Publisher = publisher;
            book.PublisherId = publisher.Id;
        }
        
        if (bookUpdate.PrimaryGenre.Name != "string")
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(p =>
                p.Name == bookUpdate.PrimaryGenre.Name || p.Id == bookUpdate.PrimaryGenre.Id);
            if (genre == null)
            {
                throw new GenreNotFoundException(
                    $"PrimaryGenre 'Name={bookUpdate.PrimaryGenre.Name}' <OR> 'ID={bookUpdate.PrimaryGenre.Id}' could not be found");
            }

            book.PrimaryGenre = genre;
            book.PrimaryGenreId = genre.Id;
        }


        book.Price = bookUpdate.Price;
        book.StockInStorage = bookUpdate.StockInStorage;
        book.OverallRating = bookUpdate.OverallRating;

        if (bookUpdate.Authors is { Count: > 0 })
        {
            var authorNames = bookUpdate.Authors.Select(a => a.Name).ToHashSet();
            var authorIds = bookUpdate.Authors.Select(a => a.Id).ToHashSet();

            var authors = await _context.Authors
                .Where(a => authorNames.Contains(a.Name) || authorIds.Contains(a.Id))
                .ToListAsync();

            if (authors.Count != bookUpdate.Authors.Count)
            {
                throw new AuthorNotFoundException("One or more authors could not be found");
            }
            book.Authors.Clear();
            book.Authors.AddRange(authors);
        }

        await _context.SaveChangesAsync();
        return EntityMapper.MapBookToBookDetail(book);
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            throw new BookNotFoundException($"Book 'ID={id}' could not be found");
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    private bool BookExists(int id)
    {
        return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    private string ErrorMessage(string entity, string name, int id)
    {
        return $"{entity} 'Name={name}' <OR> 'ID={id}' could not be found";
    }
}