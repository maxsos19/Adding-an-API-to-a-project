namespace API.Data.Models.Request.Comments
{
    public class CommentCreateRequest
    {
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string Username { get; set; }
    }
}
