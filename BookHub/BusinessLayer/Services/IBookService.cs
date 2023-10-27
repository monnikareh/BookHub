using BookHub.Models;

namespace BusinessLayer.Services;

public interface IBookService
{
    public Task<IEnumerable<BookDetail>> GetBooksAsync(string? bookName, int? genreId, string? genreName,
        int? publisherId, string? publisherName, int? authorId, string? authorName);
    public Task<BookDetail> GetBookByIdAsync(int id);
    public Task<BookDetail> CreateBookAsync(BookCreate bookCreate);
    public Task<BookDetail> UpdateBookAsync(int id, BookDetail bookDetail);
    public Task DeleteBookAsync(int id);
}