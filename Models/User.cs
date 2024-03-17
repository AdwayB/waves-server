using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace waves_server.Models;

public enum UserType {
    Admin,
    User
}

public class User {
    public Guid UserId { get; set; } = Guid.NewGuid();

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public required string Username { get; set; } = string.Empty;

    // [JsonIgnore]
    [Required]
    [MinLength(8)]
    [MaxLength(128)]
    public string Password { get; set; } = string.Empty;
    
    public string LegalName { get; set; } = string.Empty;
    
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string MobileNumber { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Type { get; set; } = UserType.User.ToString(); 
}
