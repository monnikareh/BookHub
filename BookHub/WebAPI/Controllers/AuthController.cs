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
    private readonly IAuthService _authService;

    public AuthController(BookHubDbContext context, SignInManager<User> signInManager, UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration, IAuthService authService)
    {
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