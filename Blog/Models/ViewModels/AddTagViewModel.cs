using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class AddTagViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}
