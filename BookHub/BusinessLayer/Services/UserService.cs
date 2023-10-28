using BookHub.Models;
using BusinessLayer.Exceptions;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using BusinessLayer.Mapper;

namespace BusinessLayer.Services;

public class UserService : IUserService
{
    private readonly BookHubDbContext _context;
    private readonly IUserStore<User> _userStore;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IUserEmailStore<User> _emailStore;
    
    
    public UserService(
        BookHubDbContext context, 
        IUserStore<User> userStore, 
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager)
    {
        _context = context;
        _userStore = userStore;
        _userManager = userManager;
        _roleManager = roleManager;
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        _emailStore = (IUserEmailStore<User>)_userStore;

    }
    

    public async Task<IEnumerable<UserDetail>> GetUsersAsync()
    {
        return (await _context.Users
                .Include(u => u.Orders)
                .Include(u => u.Books)
                .ToListAsync())
            .Select(EntityMapper.MapUserToUserDetail)
            .ToList();
    }

    public async Task<UserDetail> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new UserNotFoundException($"User 'ID={id}' could not be found");
        }

        return EntityMapper.MapUserToUserDetail(user);
    }

    public async Task<UserDetail> PostUserAsync(UserCreate userCreate)
    {
        User user;
        try
        {
            user = Activator.CreateInstance<User>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. ");
        }
        user.Name = userCreate.Name;
        await _userStore.SetUserNameAsync(user, userCreate.UserName, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, userCreate.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, userCreate.Password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. ");
        }
        if (await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        } 
        return EntityMapper.MapUserToUserDetail(user);
    }

    public async Task UpdateUserAsync(int id, UserCreate userCreate)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new UserNotFoundException($"User with ID {id} not found");
        }
        user.Name = userCreate.Name;
        user.UserName = userCreate.UserName;
        user.Email = userCreate.Email;
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userCreate.Password);
        await _userManager.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new UserNotFoundException($"User with ID {id} not found");
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}