using Blog.Models.Domain;

namespace Blog.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
