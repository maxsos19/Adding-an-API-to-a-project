namespace API.Data.Models.Request.Comments
{
    public class CommentEditRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ArticleId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
