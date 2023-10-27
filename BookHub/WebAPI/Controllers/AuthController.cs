using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookHub.Models;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly BookHubDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;

    public AuthController(BookHubDbContext context, SignInManager<User> signInManager, UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration, IAuthService authService)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthToken>> Login(UserSignIn userSignIn)
    {
        try
        {
            return Ok(await _authService.Login(userSignIn));

        }
        catch (Exception e)
        {
            return HandleAuthorizationException(e);
        }
    }
    

    private ActionResult HandleAuthorizationException(Exception e)
    {
        return e switch
        {
            UserNotFoundException => NotFound(e.Message),
            UnauthorizedApiAccess => Unauthorized(),
            _ => Problem("Unknown problem occured")
        };
    }
}