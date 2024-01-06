using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services;

public interface IUserService
{
    Task<IEnumerable<UserDetail>> GetUsersAsync();
    Task<UserDetail> GetUserByIdAsync(int id);
    Task<UserDetail> CreateUserAsync(UserCreate userCreate);
    Task<UserDetail> UpdateUserAsync(int id, UserUpdate userUpdate);
    Task DeleteUserAsync(int id);
    Task<(string, User user)> GetUserAsync(int id);

    Task<bool> AddBookToWishlist(int id, int bookId);
    Task<IEnumerable<BookDetail>> GetBooksInWishlist(int id);
    Task DeleteBookFromWishlist(int userId, int bookId);
}