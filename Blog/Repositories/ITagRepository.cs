using Blog.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync(string? searchQuery = null,
            string? sortBy = null,
            string? sortDirection = null,
            int pageNumber = 1, int pageSize = 100);
        
        Task<Tag?> GetAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag);

        Task<Tag?> DeleteAsync(Guid id);

        Task<int> CountAsync();

    }
}
