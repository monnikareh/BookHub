using BookHub.Models;

namespace BusinessLayer.Services;

public interface IUserService
{
    Task<IEnumerable<UserDetail>> GetUsersAsync(); 
    Task<UserDetail> GetUserByIdAsync(int id); 
    Task<UserDetail> PostUserAsync(UserCreate userCreate); 
    Task UpdateUserAsync(int id, UserCreate userCreate); 
    Task DeleteUserAsync(int id);
}