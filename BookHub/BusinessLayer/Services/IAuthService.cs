using BookHub.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services;

public interface IAuthService
{
    public Task<AuthToken> Login(UserSignIn userSignIn);
}