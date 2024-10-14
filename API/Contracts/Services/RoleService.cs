using API.Contracts.Services.IServices;
using API.Data;
using API.Data.Models.Request.Roles;
using API.Repositories;
using Microsoft.AspNetCore.Identity;

namespace API.Contracts.Services
{
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository _repo;
        


        public RoleService(IRoleRepository repo)
        {
            _repo = repo;

        }

        public async Task<IEnumerable<IdentityRole>> GetAll()
        {
            var roles = await _repo.GetAll();

            return roles;
        }
    }
}
