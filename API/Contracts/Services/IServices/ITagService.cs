using API.Data.Models.Request.Tags;
using API.Data.Models.Response.Tags;

namespace API.Contracts.Services.IServices
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllAsync();

        Task<Tag?> GetAsync(Guid id);

        Task<Tag> AddAsync(TagCreateRequest request);

        Task<Tag?> UpdateAsync(TagEditRequest request);

        Task<Tag?> DeleteAsync(Guid id);

        Task<int> CountAsync();
    }
}
