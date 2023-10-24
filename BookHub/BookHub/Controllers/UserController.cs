using BookHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BookHub.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BookHubDbContext _context;

        public UserController(BookHubDbContext context)
        {
            _context = context;
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
                .Select(ControllerHelpers.MapUserToUserDetail)
                .ToList();
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID '{id}' not found");
            }

            return user;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserDetail>> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUserById", new { id = user.Id }, user);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDetail user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            if (id != user.Id)
            {
                return BadRequest("User ID in the request does not match the ID in the data.");
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound($"User with ID {id} not found");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
