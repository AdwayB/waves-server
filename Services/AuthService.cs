using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using waves_server.Models;

namespace waves_server.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthResponse? Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new byte[64];
            if (!tokenHandler.CanReadToken(_configuration["Jwt:Key"])) ;
            {
                key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthResponse { Token = tokenHandler.WriteToken(token) };
        }
    }
}
