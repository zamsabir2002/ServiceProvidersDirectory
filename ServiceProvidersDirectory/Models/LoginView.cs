using System.ComponentModel.DataAnnotations;

namespace ServiceProvidersDirectory.Models
{
    public class LoginView
    {
        [Required]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
