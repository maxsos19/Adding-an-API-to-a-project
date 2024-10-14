using Blog.Data;
using Blog.Models.Domain;

namespace Blog.Repositories
{
    public interface ICommentRepository
    {
        Task<ArticlesComment> AddAsync(ArticlesComment articlesComment);

        Task<IEnumerable<ArticlesComment>> GetCommentsByArticleIdAsync(Guid articleId);

        Task<IEnumerable<ArticlesComment>> GetAllAsync();

        Task<ArticlesComment?> DeleteAsync(Guid id);

        Task<ArticlesComment?> UpdateAsync(ArticlesComment articlesComment);

        Task<ArticlesComment?> GetAsync(Guid id);
       
    }
}
