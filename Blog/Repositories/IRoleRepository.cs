using Microsoft.AspNetCore.Identity;

namespace Blog.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IdentityRole>> GetAll();
    }
}
