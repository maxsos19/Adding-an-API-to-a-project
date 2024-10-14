using Blog.Models.Domain;

namespace Blog.Repositories
{
    public interface IArticleLikesRepository
    {
        Task<int> GetTotalLikes(Guid articleId);


        Task<IEnumerable<ArticleLike>> GetLikesForArticle(Guid articleId);

        Task<ArticleLike> AddLikeForArticle(ArticleLike articleLike);
    }
}
