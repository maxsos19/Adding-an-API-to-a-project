

using API.Data;
using API.Data.Models.Response.ArticlesLikes;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ArticleLikesRepository : IArticleLikesRepository
    {
        private readonly BlogDbContext blogDbContext;

        public ArticleLikesRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        public async Task<ArticleLike> AddLikeForArticle(ArticleLike articleLike)
        {
            await blogDbContext.ArticlesLikes.AddAsync(articleLike);
            await blogDbContext.SaveChangesAsync();
            return articleLike;
        }

        public async Task<IEnumerable<ArticleLike>> GetLikesForArticle(Guid articleId)
        {
            return await blogDbContext.ArticlesLikes.Where(x => x.ArticleId == articleId).ToListAsync();


        }

        public async Task<int> GetTotalLikes(Guid articleId)
        {
            return await blogDbContext.ArticlesLikes.CountAsync(x => x.ArticleId == articleId);
        }
    }
}
