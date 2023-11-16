using BusinessLayer.Exceptions;
using BusinessLayer.Models;
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

    public BookService(BookHubDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<BookDetail>> GetBooksAsync(string? bookName, int? genreId,
        string? genreName,
        int? publisherId, string? publisherName, int? authorId, string? authorName)
    {
        var books = _context.Books
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
        var book = await _context
            .Books
            .Include(g => g.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
        {
            throw new BookNotFoundException($"Book 'ID={id}' could not be found");
        }

        return EntityMapper.MapBookToBookDetail(book);
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

        var genreNames = bookCreate.Genres.Select(g => g.Name).ToHashSet();
        var genreIds = bookCreate.Genres.Select(g => g.Id).ToHashSet();
        var genres = await _context.Genres.Where(g => genreNames.Contains(g.Name) || genreIds.Contains(g.Id)).ToListAsync();
        
        if (genres.Count != bookCreate.Genres.Count)
        {
            throw new GenreNotFoundException("One or more genres could not be found");
        }
        
        var authorNames = bookCreate.Authors.Select(a => a.Name).ToHashSet();
        var authorIds = bookCreate.Authors.Select(a => a.Id).ToHashSet();
        var authors = await _context.Authors.Where(a => authorNames.Contains(a.Name) || authorIds.Contains(a.Id)).ToListAsync();
        
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
            Price = bookCreate.Price,
            StockInStorage = bookCreate.StockInStorage,
            OverallRating = bookCreate.OverallRating
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return EntityMapper.MapBookToBookDetail(book);
    }

    public async Task<BookDetail> UpdateBookAsync(int id, BookDetail bookDetail)
    {
        var book = await _context.Books
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            throw new BookNotFoundException($"Book 'ID={id}' could not be found");
        }

        book.Name = bookDetail.Name;

        if (bookDetail.Genres.Count > 0)
        {
            var genreNames = bookDetail.Genres.Select(g => g.Name).ToHashSet();
            var genreIds = bookDetail.Genres.Select(g => g.Id).ToHashSet();

            var genres = await _context.Genres
                .Where(g => genreNames.Contains(g.Name) || genreIds.Contains(g.Id))
                .ToListAsync();

            if (genres.Count != bookDetail.Genres.Count)
            {
                throw new GenreNotFoundException("One or more genres could not be found");
            }
            book.Genres.Clear();
            book.Genres.AddRange(genres);
        }

        if (bookDetail.Publisher.Name != "string")
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p =>
                p.Name == bookDetail.Publisher.Name || p.Id == bookDetail.Publisher.Id);
            if (publisher == null)
            {
                throw new PublisherNotFoundException(
                    $"Publisher 'Name={bookDetail.Publisher.Name}' <OR> 'ID={bookDetail.Publisher.Id}' could not be found");
            }

            book.Publisher = publisher;
            book.PublisherId = publisher.Id;
        }

        book.Price = bookDetail.Price;
        book.StockInStorage = bookDetail.StockInStorage;
        book.OverallRating = bookDetail.OverallRating;

        if (bookDetail.Authors is { Count: > 0 })
        {
            var authorNames = bookDetail.Authors.Select(a => a.Name).ToHashSet();
            var authorIds = bookDetail.Authors.Select(a => a.Id).ToHashSet();

            var authors = await _context.Authors
                .Where(a => authorNames.Contains(a.Name) || authorIds.Contains(a.Id))
                .ToListAsync();

            if (authors.Count != bookDetail.Authors.Count)
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