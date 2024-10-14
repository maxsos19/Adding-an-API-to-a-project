

using API.Data.Models.Response.Articles;

namespace API.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllAsync();

        Task<Article?> GetAsync(Guid id);

        Task<Article> AddAsync(Article article);

        Task<Article?> UpdateAsync(Article article);

        Task<Article?> DeleteAsync(Guid id);

        Task<Article?> GetByUrlHandleAsync(string urlHandle);

        Task<IEnumerable<Article>> GetArticlesByAuthorAsync(string userName);
    }
}
