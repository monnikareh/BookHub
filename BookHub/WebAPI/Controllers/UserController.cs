using BookHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BusinessLayer.Mapper;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BookHubDbContext _context;
        private readonly IUserStore<User> _userStore;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IUserEmailStore<User> _emailStore;



        public UserController(
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


        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return (await _context.Users
                    .Include(u => u.Orders)
                    .Include(u => u.Books)
                    .ToListAsync())
                .Select(EntityMapper.MapUserToUserDetail)
                .ToList();
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<UserDetail>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID '{id}' not found");
            }

            return EntityMapper.MapUserToUserDetail(user);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserDetail>> PostUser(UserCreate userCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

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

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserCreate userCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"Order with ID {id} not found");
            }
            user.Name = userCreate.Name;
            user.UserName = userCreate.UserName;
            user.Email = userCreate.Email;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userCreate.Password);

            await _userManager.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"Order with ID {id} not found");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
