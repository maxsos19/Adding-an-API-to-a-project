

using API.Data.Models.Response.ArticlesLikes;

namespace API.Repositories
{
    public interface IArticleLikesRepository
    {
        Task<int> GetTotalLikes(Guid articleId);


        Task<IEnumerable<ArticleLike>> GetLikesForArticle(Guid articleId);

        Task<ArticleLike> AddLikeForArticle(ArticleLike articleLike);
    }
}
