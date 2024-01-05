using System.Text;
using BusinessLayer.Exceptions;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;

namespace BusinessLayer.Services;

public class UserService : IUserService
{
    private readonly BookHubDbContext _context;
    private readonly IUserStore<User> _userStore;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IUserEmailStore<User> _emailStore;
    private readonly IMemoryCache _memoryCache;


    public UserService(
        BookHubDbContext context,
        IUserStore<User> userStore,
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager, IMemoryCache memoryCache)
    {
        _context = context;
        _userStore = userStore;
        _userManager = userManager;
        _roleManager = roleManager;
        _memoryCache = memoryCache;
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }

        _emailStore = (IUserEmailStore<User>)_userStore;
    }


    public async Task<IEnumerable<UserDetail>> GetUsersAsync()
    {
        var users = await _context.Users
            .Include(u => u.Orders)
            .Include(u => u.Books)
            .ToListAsync();
        return users.Select(EntityMapper.MapUserToUserDetail);
    }

    public async Task<UserDetail> GetUserByIdAsync(int id)
    {
        var key = $"BookById_{id}";
        if (_memoryCache.TryGetValue(key, out UserDetail? cached) && cached is not null)
        {
            return cached;
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new UserNotFoundException($"User 'ID={id}' could not be found");
        }

        var mapped = EntityMapper.MapUserToUserDetail(user);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
        _memoryCache.Set(key, mapped, cacheEntryOptions);
        return mapped;
    }

    public async Task<UserDetail> CreateUserAsync(UserCreate userCreate)
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

        ICollection<Book> books = new List<Book>();
        if (!userCreate.Books.IsNullOrEmpty())
        {
            var bookIds = userCreate.Books.Select(a => a.Id).ToHashSet();
            var bookNames = userCreate.Books.Select(a => a.Name).ToHashSet();
            books = await _context.Books.Where(a => bookNames.Contains(a.Name) || bookIds.Contains(a.Id)).ToListAsync();
            if (books.Count != userCreate.Books.Count)
            {
                throw new BooksEmptyException("One or more books could not be found");
            }
        }
        
        user.Name = userCreate.Name;
        user.Books = books;
        await _userStore.SetUserNameAsync(user, userCreate.UserName, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, userCreate.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, userCreate.Password);
        if (!result.Succeeded)
        {
            var errors = new StringBuilder();
            foreach (var err in result.Errors)
            {
                errors.Append($"{err.Code} - {err.Description}\n");
            }

            throw new UserAlreadyExistsException($"User could not be created: {errors.ToString()}");
        }

        if (await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }

        return EntityMapper.MapUserToUserDetail(user);
    }

    public async Task<UserDetail> UpdateUserAsync(int id, UserUpdate userUpdate)
    {
        var user = await _context.Users
            .Include(b => b.Books)
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (user == null)
        {
            throw new UserNotFoundException($"User with ID {id} not found");
        }

        user.Name = userUpdate.Name;
        if (userUpdate.Books is { Count: > 0 })
        {
            var bookNames = userUpdate.Books.Select(a => a.Name).ToHashSet();
            var bookIds = userUpdate.Books.Select(a => a.Id).ToHashSet();

            var books = await _context.Books
                .Where(a => bookNames.Contains(a.Name) || bookIds.Contains(a.Id))
                .ToListAsync();

            if (books.Count != userUpdate.Books.Count)
            {
                throw new BooksEmptyException("One or more books could not be found");
            }
            user.Books.Clear();
            user.Books.AddRange(books);
        }
        await _userManager.SetUserNameAsync(user, userUpdate.UserName);
        await _userManager.SetEmailAsync(user, userUpdate.Email);
        await _userManager.ChangePasswordAsync(user, userUpdate.OldPassword, userUpdate.NewPassword);
        await _userManager.UpdateAsync(user);
        await _context.SaveChangesAsync();
        return EntityMapper.MapUserToUserDetail(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new UserNotFoundException($"User with ID {id} not found");
        }

        await _userManager.DeleteAsync(user);
    }
    
    public async Task<(string, User user)> GetUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new UserNotFoundException($"User with ID {id} not found");
        }

        return (await _userManager.GeneratePasswordResetTokenAsync(user), user);
    }
    
    public async Task<bool> AddBookToWishlist(int id, int bookId)
    {
        var user = await _context.Users
            .Include(b => b.Books)
            .FirstOrDefaultAsync(u => u.Id == id);
        
        if (user == null)
        {
            throw new UserNotFoundException($"User with ID {id} not found");
        }
        var toBeAddedBook =  _context.Books.First(b => b.Id == bookId);
        
        if (toBeAddedBook == null)
        {
            throw new BookNotFoundException($"Book with ID {bookId} not found");
        }
        
        if (user.Books.Any(b => b.Id == bookId))
        {
            return false;
        }
        
        user.Books.Add(toBeAddedBook);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<BookDetail>> GetBooksInWishlist(int id)
    {
        var user = await _context
                .Users
                .Include(b => b.Books)
                .ThenInclude(pg => pg.PrimaryGenre)
                .Include(b => b.Books)
                .ThenInclude(g => g.Genres)
                .Include(b => b.Books)
                .ThenInclude(b => b.Publisher)
                .Include(b => b.Books)
                .ThenInclude(b => b.Authors)
                .FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            throw new UserNotFoundException($"User with ID {id} not found");
        }

        return user.Books.Select(EntityMapper.MapBookToBookDetail);
    }
    
    public async Task DeleteBookFromWishlist(int userId, int bookId)
    {
        var user = await _context
            .Users
            .Include(b => b.Books)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new UserNotFoundException($"User with ID {userId} not found");
        }

        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
        {
            throw new BookNotFoundException($"Book with ID {bookId} not found");
        }
        user.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}