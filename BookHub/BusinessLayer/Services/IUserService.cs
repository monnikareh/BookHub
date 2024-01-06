using BusinessLayer.Errors;
using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services;

public interface IUserService
{
    Task<IEnumerable<UserDetail>> GetUsersAsync();
    Task<Result<UserDetail, string>> GetUserByIdAsync(int id);
    Task<Result<UserDetail, string>> CreateUserAsync(UserCreate userCreate);
    Task<Result<UserDetail, string>> UpdateUserAsync(int id, UserUpdate userUpdate);
    Task<Result<bool, string>> DeleteUserAsync(int id);
    Task<Result<(string, User user), string>> GetUserAsync(int id);

    Task<Result<bool, string>> AddBookToWishlist(int id, int bookId);
    Task<Result<IEnumerable<BookDetail>, string>> GetBooksInWishlist(int id);
    Task<Result<bool, string>> DeleteBookFromWishlist(int userId, int bookId);
}