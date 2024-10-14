using API.Contracts.Services.IServices;
using API.Data.Models.Request.Comments;
using API.Data.Models.Response.Comments;
using API.Repositories;

namespace API.Contracts.Services
{
    public class CommentService : ICommentService
    {

        private readonly ICommentRepository _repo;
       

        public CommentService(ICommentRepository repo)
        {
            _repo = repo;
           
        }

        public Task<ArticlesComment> AddAsync(ArticlesComment articlesComment)
        {
            throw new NotImplementedException();
        }

        public Task<ArticlesComment?> DeleteAsync(Guid id)
        {
           return _repo.DeleteAsync(id);
        }

        public Task<IEnumerable<ArticlesComment>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }

        public Task<ArticlesComment?> GetAsync(Guid id)
        {
            return _repo.GetAsync(id);
        }

        public Task<IEnumerable<ArticlesComment>> GetCommentsByArticleIdAsync(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public async Task<ArticlesComment> UpdateAsync(CommentEditRequest request)
        {
            var comment = new ArticlesComment
            {
                Id = request.Id,
                Description = request.Description,
                ArticleId = request.ArticleId,
                UserId = request.UserId,
                DateAdded = request.DateAdded
            };

            var updatedComment = await _repo.UpdateAsync(comment);

            return updatedComment;
        }
    }
}
