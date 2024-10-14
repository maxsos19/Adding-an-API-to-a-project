using System.ComponentModel.DataAnnotations;

namespace API.Data.Models.Request.Tags
{
    public class TagCreateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}
