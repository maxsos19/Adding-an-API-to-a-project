namespace Blog.Models.Domain
{
    public class ArticlesComment
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ArticleId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
