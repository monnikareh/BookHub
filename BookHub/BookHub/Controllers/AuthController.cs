using BookHub.Models;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookHub.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly BookHubDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AuthController(BookHubDbContext context, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<User>>> GetToken(UserSignIn userSignIn)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userSignIn.UserName);
        if (user == null)
        {
            return NotFound($"User {userSignIn.UserName} not found");
        }

        var a = await _signInManager.PasswordSignInAsync(userSignIn.UserName, userSignIn.Password, false, false);
        if (a.Succeeded)
        {
        }

        return await _userManager.Users.ToListAsync();
    }
}