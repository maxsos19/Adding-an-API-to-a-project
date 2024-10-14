using API.Contracts.Services.IServices;
using API.Repositories;
using Microsoft.AspNetCore.Identity;

namespace API.Contracts.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repo;
        

        public UserService(IUserRepository repo)
        {
            _repo = repo;
            
        }
        public Task<IEnumerable<IdentityUser>> GetAll()
        {
           var users = _repo.GetAll();
           return users;
        }
    }
}
