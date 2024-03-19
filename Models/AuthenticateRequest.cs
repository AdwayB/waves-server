using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace waves_server.Models;

public class AuthenticateRequest {
    [Required]
    [DefaultValue("geneva.quests@gmail.com")]
    [EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9.+_%$#&-]+@gmail\.com$")]
    public string Email { get; set; } = string.Empty;
   
    [Required]
    [DefaultValue("System")]
    public required string Password { get; set; } = string.Empty;
    
    [Required]
    [DefaultValue("user")]
    public required string Type {get; set;} = UserType.User.ToString(); 
}
