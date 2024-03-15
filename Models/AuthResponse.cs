using System.ComponentModel.DataAnnotations;

namespace waves_server.Models
{
    public class AuthResponse
    {
        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; } = string.Empty;
    }
}