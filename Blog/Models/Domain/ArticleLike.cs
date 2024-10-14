namespace Blog.Models.Domain
{
    public class ArticleLike
    {
        public Guid Id { get; set; }

        public Guid ArticleId { get; set; }

        public Guid UserId { get; set; }

    }
}
