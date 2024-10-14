using Microsoft.AspNetCore.Identity;

namespace API.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IdentityRole>> GetAll();
    }
}
