namespace API.Data.Models.Response.ArticlesLikes
{
    public class ArticleLike
    {
        public Guid Id { get; set; }

        public Guid ArticleId { get; set; }

        public Guid UserId { get; set; }
    }
}
