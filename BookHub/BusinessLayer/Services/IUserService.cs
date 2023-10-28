using BookHub.Models;

namespace BusinessLayer.Services;

public interface IUserService
{
    public Task<IEnumerable<UserDetail>> GetUsersAsync();

    public Task<UserDetail?> GetUserByIdAsync(int id);

    public Task<UserDetail> PostUserAsync(UserCreate userCreate);

    public Task<bool> UpdateUserAsync(int id, UserCreate userCreate);

    public Task<bool> DeleteUserAsync(int id);
}