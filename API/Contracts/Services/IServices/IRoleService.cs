using API.Data.Models.Response.Roles;
using Microsoft.AspNetCore.Identity;

namespace API.Contracts.Services.IServices
{
    public interface IRoleService
    {
        Task<IEnumerable<IdentityRole>> GetAll();

        //Task<List<Role>> GetRoles();
    }
}
