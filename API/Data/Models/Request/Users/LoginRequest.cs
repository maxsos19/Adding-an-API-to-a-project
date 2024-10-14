using System.ComponentModel.DataAnnotations;

namespace API.Data.Models.Request.Users
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password has to be at least 6 characters")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
