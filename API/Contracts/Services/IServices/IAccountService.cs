using API.Data.Models.Request.Users;
using Microsoft.AspNetCore.Identity;

namespace API.Contracts.Services.IServices
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(RegisterRequest request);

        Task<SignInResult> Login(LoginRequest request);
    }
}
