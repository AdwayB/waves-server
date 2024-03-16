using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace waves_server.Models;

public class AuthenticateRequest
{
    [Required]
    [DefaultValue("System")]
    public required string Username { get; set; } = string.Empty;
   
    [Required]
    [DefaultValue("System")]
    public required string Password { get; set; } = string.Empty;
}
