using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к AccountController");
        }

        [HttpGet]
        public IActionResult Register()
        {
            _logger.LogInformation("AccountController - обращение к методу Register");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email
                };

                var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    // assign this user the "User" role
                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                    if (roleIdentityResult.Succeeded)
                    {
                        // Show success notification
                        _logger.LogInformation("AccountController - обращение к методу Register");
                        return RedirectToAction("Register");
                    }
                }
            }

            // Show error notification
            _logger.LogInformation("AccountController - обращение к методу Register");
            return View();
        }


        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel { ReturnUrl = ReturnUrl };
            _logger.LogInformation("AccountController - обращение к методу Login");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
                _logger.LogInformation("AccountController - обращение к методу Login");
                return RedirectToAction("Index", "Home");

            }
            _logger.LogInformation("AccountController - обращение к методу Login");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await signInManager.SignOutAsync();
            _logger.LogInformation("AccountController - обращение к методу Logout");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            _logger.LogInformation("AccountController - обращение к методу AccessDenied");
            return View();
            
        }
    }
}
