using BusinessLayer.Errors;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IBookService
{
    public Task<IEnumerable<BookDetail>> GetBooksAsync(string? bookName, int? genreId, string? genreName,
        int? publisherId, string? publisherName, int? authorId, string? authorName);

    Task<Result<BookDetail, string>> GetBookByIdAsync(int id);
    Task<Result<BookDetail, string>> CreateBookAsync(BookCreate bookCreate);
    Task<Result<BookDetail, string>> UpdateBookAsync(int id, BookCreate bookUpdate);
    Task<Result<bool, string>> DeleteBookAsync(int id);
}