using BookHub.Models;

namespace BusinessLayer.Services;

public interface IBookService
{
    public Task<IEnumerable<BookDetail>> GetBooks(int? bookId, string? bookName, int? genreId, string? genreName,
        int? publisherId, string? publisherName, int? authorId, string? authorName);
    public Task<BookDetail> GetBookById(int id);
    public Task<BookDetail> PostBook(BookCreate bookCreate);
    public Task<BookDetail> UpdateBook(int id, BookDetail bookDetail);
    public Task DeleteBook(int id);
}