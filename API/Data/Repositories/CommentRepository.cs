
using API.Data;
using API.Data.Models.Response.Comments;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogDbContext blogDbContext;

        public CommentRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        public async Task<ArticlesComment> AddAsync(ArticlesComment articlesComment)
        {
           await blogDbContext.Comments.AddAsync(articlesComment);
           await blogDbContext.SaveChangesAsync();
           return articlesComment;
        }

        public async Task<ArticlesComment?> DeleteAsync(Guid id)
        {
            var existingComment = await blogDbContext.Comments.FindAsync(id);

            if (existingComment != null)
            {
                blogDbContext.Comments.Remove(existingComment);
                await blogDbContext.SaveChangesAsync();
                return existingComment;
            }

            return null;
        }

        public async Task<IEnumerable<ArticlesComment>> GetAllAsync()
        {
            return await blogDbContext.Comments.ToListAsync();
        }

        public async Task<IEnumerable<ArticlesComment>> GetCommentsByArticleIdAsync(Guid articleId)
        {
            return await blogDbContext.Comments.Where(x => x.ArticleId == articleId).ToListAsync();
        }

        public async Task<ArticlesComment?> UpdateAsync(ArticlesComment articlesComment)
        {
            var existingComment = await blogDbContext.Comments.FirstOrDefaultAsync(x => x.Id == articlesComment.Id);

            if (existingComment != null)
            {
                existingComment.Id = articlesComment.Id;
                existingComment.Description = articlesComment.Description;
                existingComment.ArticleId = articlesComment.ArticleId;
                existingComment.UserId = articlesComment.UserId;
                existingComment.DateAdded = articlesComment.DateAdded;
               
                await blogDbContext.SaveChangesAsync();
                return existingComment;
            }

            return null;
        }


        public async Task<ArticlesComment?> GetAsync(Guid id)
        {
            return await blogDbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
