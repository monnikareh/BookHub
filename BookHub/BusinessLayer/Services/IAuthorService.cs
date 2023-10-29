using BookHub.Models;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDetail>> GetAuthorsAsync(string? name, int? bookId, string? bookName);
    Task<AuthorDetail> GetAuthorByIdAsync(int id);
    Task<AuthorDetail> PostAuthorAsync(AuthorCreate authorCreate);
    Task<AuthorDetail> UpdateAuthorAsync(int id, AuthorUpdate authorUpdate);
    Task DeleteAuthorAsync(int id);

}