using API.Contracts.Services.IServices;
using API.Data.Models.Request.Users;
using Microsoft.AspNetCore.Identity;

namespace API.Contracts.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;


        public AccountService(UserManager<IdentityUser> userManager,
           SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
           
        }

        public async Task<SignInResult> Login(LoginRequest request)
        {
            var signInResult = await signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                //{
                //    if (!string.IsNullOrWhiteSpace(request.ReturnUrl))
                //    {
                //        return Redirect(loginViewModel.ReturnUrl);
                //    }
                //    _logger.LogInformation("AccountController - обращение к методу Login");
                //    return RedirectToAction("Index", "Home");

            }

            return signInResult;
            
        }

        public async Task<IdentityResult> Register(RegisterRequest request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, request.Password);

            if (identityResult.Succeeded)
            {
                // assign this user the "User" role
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                return identityResult;
            }
            else
            {
                return identityResult;
            }
        }
    }
}
