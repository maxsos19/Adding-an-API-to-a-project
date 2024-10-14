using API.Data.Models.Request.Comments;
using API.Data.Models.Response.Comments;

namespace API.Contracts.Services.IServices
{
    public interface ICommentService
    {
        Task<ArticlesComment> AddAsync(ArticlesComment articlesComment);

        Task<IEnumerable<ArticlesComment>> GetCommentsByArticleIdAsync(Guid articleId);

        Task<IEnumerable<ArticlesComment>> GetAllAsync();

        Task<ArticlesComment?> DeleteAsync(Guid id);

        Task<ArticlesComment?> UpdateAsync(CommentEditRequest request);

        Task<ArticlesComment?> GetAsync(Guid id);

    }
}
