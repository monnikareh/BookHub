using BusinessLayer.Errors;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDetail>> GetAuthorsAsync(string? name, int? bookId, string? bookName);
    Task<Result<AuthorDetail, string>> GetAuthorByIdAsync(int id);
    Task<AuthorDetail> CreateAuthorAsync(AuthorCreate authorCreate);
    Task<Result<AuthorDetail, string>> UpdateAuthorAsync(int id, AuthorUpdate authorUpdate);
    Task<Result<bool, string>> DeleteAuthorAsync(int id);
}