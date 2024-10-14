namespace Blog.Models.ViewModels
{
    public class EditCommentViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ArticleId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
