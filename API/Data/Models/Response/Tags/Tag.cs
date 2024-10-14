using API.Data.Models.Response.Articles;

namespace API.Data.Models.Response.Tags
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
