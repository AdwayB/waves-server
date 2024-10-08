﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using waves_server.Helpers;
using waves_server.Models;

namespace waves_server.Services {
  public class AuthService : IAuthService {
    private readonly AppSettings _appSettings;
    private readonly DatabaseContext _db;

    public AuthService(IOptions<AppSettings> appSettings, DatabaseContext db) {
      _appSettings = appSettings.Value;
      _db = db;
    }

    public async Task<(AuthenticateResponse?, int)> SignUp(User model, UserType type = UserType.User) {
      if (await _db.Users.AnyAsync(u => u.Username == model.Username && u.Type == type.ToString())) {
        return (null, -2);
      } 
      if (await _db.Users.AnyAsync(u => u.Email == model.Email && u.Type == type.ToString())) {
        return (null, -1);
      }

      var user = new User {
        Username = model.Username,
        Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
        LegalName = model.LegalName,
        Email = model.Email,
        MobileNumber = model.MobileNumber,
        Country = model.Country,
        Type = type.ToString()
      };

      await _db.Users.AddAsync(user);
      await _db.SaveChangesAsync();

      var token = await GenerateJwtToken(user);
      return (new AuthenticateResponse(user, token), 0);
    }

    public async Task<(AuthenticateResponse?, int)> Authenticate(AuthenticateRequest model, UserType type = UserType.User) {
      var user = await _db.Users.SingleOrDefaultAsync(x => x.Email == model.Email && x.Type == type.ToString());

      if (user == null) {
        return (null, -1);
      }
      
      if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password)) {
        return (null, -2);
      }

      var token = await GenerateJwtToken(user);

      return (new AuthenticateResponse(user, token), 0);
    }
    
    public async Task<(string, int)> Logout(AuthenticateRequest model, UserType type = UserType.User) {
      var user = await _db.Users.SingleOrDefaultAsync(x => x.Email == model.Email && x.Type == type.ToString());

      return user == null ? (string.Empty, -1) : ("valid", 1);
    }

    private async Task<string> GenerateJwtToken(User user) {
      var tokenHandler = new JwtSecurityTokenHandler();
      
      var token = await Task.Run(() => {
        var key = Encoding.ASCII.GetBytes(_appSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(new[] {
            new Claim("userId", user.UserId.ToString()), 
            new Claim(type: "type", value: user.Type)
          }),
          Expires = DateTime.UtcNow.AddSeconds(_appSettings.Duration),
          Issuer = _appSettings.Issuer,
          Audience = _appSettings.Audience,
          SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature
          )
        };
        
        return tokenHandler.CreateToken(tokenDescriptor);
      });

      return tokenHandler.WriteToken(token);
    }
  }
}
