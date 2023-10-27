using BusinessLayer.Exceptions;

namespace BusinessLayer.Services;

using BookHub.Models;
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


    public async Task<IEnumerable<BookDetail>> GetBooksAsync(int? bookId, string? bookName, int? genreId,
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

        if (bookId is > 0)
        {
            books = books.Where(b => b.Id == bookId);
        }
        
        if (!bookName.IsNullOrEmpty())
        {
            books = books.Where(b => b.Name == bookName);
        }

        return await books.Select(b => ControllerHelpers.MapBookToBookDetail(b)).ToListAsync();
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

        return ControllerHelpers.MapBookToBookDetail(book);
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

        var genres = new List<Genre>();
        foreach (var genreRelatedModel in bookCreate.Genres)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g =>
                g.Name == genreRelatedModel.Name || g.Id == genreRelatedModel.Id);
            if (genre == null)
            {
                throw new GenreNotFoundException(
                    $"Genre 'Name={genreRelatedModel.Name}' <OR> 'ID={genreRelatedModel.Id}' could not be found");
            }

            genres.Add(genre);
        }

        var authors = new List<Author>();
        foreach (var authorRelatedModel in bookCreate.Authors)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a =>
                a.Name == authorRelatedModel.Name || a.Id == authorRelatedModel.Id);
            if (author == null)
            {
                throw new AuthorNotFoundException(
                    $"Author 'Name={authorRelatedModel.Name}' <OR> 'ID={authorRelatedModel.Id}' could not be found");
            }

            authors.Add(author);
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
        return ControllerHelpers.MapBookToBookDetail(book);
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
            book.Genres.Clear();
            foreach (var genreRelatedModel in bookDetail.Genres)
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(g =>
                    g.Name == genreRelatedModel.Name || g.Id == genreRelatedModel.Id);
                if (genre == null)
                {
                    throw new GenreNotFoundException(
                        $"Genre 'Name={genreRelatedModel.Name}' <OR> 'ID={genreRelatedModel.Id}' could not be found");
                }

                book.Genres.Add(genre);
            }
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
            book.Authors.Clear();
            foreach (var authorRelatedModel in bookDetail.Authors)
            {
                var author = await _context.Authors.FirstOrDefaultAsync(a =>
                    a.Name == authorRelatedModel.Name || a.Id == authorRelatedModel.Id);
                if (author == null)
                {
                    throw new AuthorNotFoundException(
                        $"Author 'Name={authorRelatedModel.Name}' <OR> 'ID={authorRelatedModel.Id}' could not be found");
                }

                book.Authors.Add(author);
            }
        }

        try
        {
            await _context.SaveChangesAsync();
            return ControllerHelpers.MapBookToBookDetail(book);
        }
        catch (Exception ex)
        {
            throw new EntityUpdateException($"Error updating book: {ex.Message}");
        }
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