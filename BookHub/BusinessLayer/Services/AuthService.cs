using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookHub.Models;
using BusinessLayer.Exceptions;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.Services;

public class AuthService : IAuthService
{
    private readonly BookHubDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(BookHubDbContext context, SignInManager<User> signInManager, UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }
    
    public async Task<AuthToken> Login(UserSignIn userSignIn)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userSignIn.UserName);
        if (user == null)
        {
            throw new UserNotFoundException($"User {userSignIn.UserName} not found");
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
        var token = GetToken(authClaims);
        var signIn = await _signInManager.PasswordSignInAsync(userSignIn.UserName, userSignIn.Password, false, false);
        if (signIn.Succeeded)
        {
            return new AuthToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        throw new UnauthorizedApiAccess();
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}