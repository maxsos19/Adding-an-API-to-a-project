using Microsoft.AspNetCore.Identity;

namespace API.Contracts.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
