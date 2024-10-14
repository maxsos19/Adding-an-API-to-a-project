namespace API.Data.Models.Request.ArticlesLikes
{
    public class LikeCreateRequest
    {
        public Guid ArticleId { get; set; }

        public Guid UserId { get; set; }
    }
}
