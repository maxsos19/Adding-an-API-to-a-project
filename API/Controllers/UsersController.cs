using API.Contracts.Services.IServices;
using API.Data.Models.Request.Users;
using API.Data.Models.Response.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> userManager;

        public UsersController(IUserService userService)
        {
            _userService = userService;

        }


        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<IdentityUser>> List()
        {
            var users = await _userService.GetAll();
            
            return users;
        }


        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                var identityResult = await userManager.DeleteAsync(user);

                if (identityResult is not null && identityResult.Succeeded)
                {
                    return Ok(); 
                }
            }

            
            return NotFound();
        }

    }
}
