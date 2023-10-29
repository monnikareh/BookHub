using BookHub.Models;
using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services;

public interface IAuthService
{
    public Task<AuthToken> Login(UserSignIn userSignIn);
}