using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager, ILogger<UsersController> logger)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к UsersController");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();

            var userViewModel = new UserViewModel();
            userViewModel.Users = new List<User>();

            foreach (var user in users)
            {
                userViewModel.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    EmailAddress = user.Email,
                });
            }
            _logger.LogInformation("UsersController - обращение к методу List");
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel userViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = userViewModel.Username,
                Email = userViewModel.Email

            };

            var identityResult = await userManager.CreateAsync(identityUser, userViewModel.Password);

            if (identityResult is not null)
            {
                if (identityResult.Succeeded)
                {
                    var roles = new List<string> { "User" };

                    if (userViewModel.AdminRoleCheckbox)
                    {
                        roles.Add("Admin");
                    }

                    identityResult = await userManager.AddToRolesAsync(identityUser, roles);

                    if (identityResult is not null && identityResult.Succeeded )
                    {
                        //_logger.LogInformation("UsersController - обращение к методу List");
                        return RedirectToAction("List", "Users");
                    }
                }
            }
            _logger.LogInformation("UsersController - обращение к методу List");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                var identityResult = await userManager.DeleteAsync(user);

                if (identityResult is not null && identityResult.Succeeded)
                {
                   // _logger.LogInformation("UsersController - обращение к методу Delete");
                    return RedirectToAction("List", "Users");
                }
            }
            _logger.LogInformation("UsersController - обращение к методу List");
            return View();
        }
    }
}
