using API.Contracts.Services.IServices;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{


    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IRoleService roleService;
       

        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
           

        }
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<IdentityRole>> List()
        {
            var roles = await roleService.GetAll();

            
            return roles;
        }
    }
}
