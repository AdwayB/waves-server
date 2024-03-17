using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace waves_server.Models;

public enum UserType {
    Admin,
    User
};

public class User {
    public Guid UserId { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Username { get; set; } = string.Empty;
  
    // [JsonIgnore]
    [Required]
    [MinLength(8)]
    [MaxLength(120)]
    public string Password { get; set; } = string.Empty;
  
    [Required]
    [MinLength(4)]
    [MaxLength(20)]
    public string LegalName { get; set; } = string.Empty;
  
    [Required]
    [MinLength(4)]
    [MaxLength(30)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
  
    [Required]
    [MinLength(7)]
    [MaxLength(15)]
    public string MobileNumber { get; set; } = string.Empty;
  
    [Required]
    [MinLength(3)]
    [MaxLength(3)]
    public string Country { get; set; } = string.Empty;

    [Required]
    [MinLength(4)]
    [MaxLength(5)]
    public string Type { get; set; } = UserType.User.ToString();
}
