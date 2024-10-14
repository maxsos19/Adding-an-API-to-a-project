using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{

    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {

        private readonly IRoleRepository roleRepository;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRoleRepository roleRepository, ILogger<RolesController> logger)
        {
            this.roleRepository = roleRepository;
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к RolesController");

        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var roles = await roleRepository.GetAll();

            var rolesViewModels = new List<RoleViewModel>();

            foreach (var role in roles)
            {
                var roleViewModel = new RoleViewModel
                {
                    Id = Guid.Parse(role.Id),
                    Name = role.Name
                };

                rolesViewModels.Add(roleViewModel);
            }


            _logger.LogInformation("RolesController - обращение к методу List");
            return View(rolesViewModels);
        }
    }
}
