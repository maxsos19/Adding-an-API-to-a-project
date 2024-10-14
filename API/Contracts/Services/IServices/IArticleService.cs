using API.Data.Models.Request.Articles;
using API.Data.Models.Response.Articles;

namespace API.Contracts.Services.IServices
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAllAsync();

        Task<Article?> GetAsync(Guid id);

        Task<Article> AddAsync(ArticleCreateRequest request);

        Task<Article?> UpdateAsync(ArticleEditRequest request);

        Task<Article?> DeleteAsync(ArticleEditRequest request);

        
    }
}
