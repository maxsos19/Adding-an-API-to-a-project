using Blog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories
{
    public class RoleRepository : IRoleRepository
    {

        private readonly AuthDbContext authDbContext;

        public RoleRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }


        public async Task<IEnumerable<IdentityRole>> GetAll()
        {
            var roles = await authDbContext.Roles.ToListAsync();

            
            return roles;
        }
    }
}
