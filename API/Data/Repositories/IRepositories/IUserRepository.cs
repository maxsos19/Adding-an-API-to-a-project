using Microsoft.AspNetCore.Identity;

namespace API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
