using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using waves_server.Models;
using waves_server.Services;

namespace waves_server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase {
  private readonly IAuthService _authService;
  private static void SetCookies (AuthenticateResponse result, HttpResponse Response) {
    Response.Cookies.Append("jwt", result.Token, new CookieOptions { 
      HttpOnly = true,
      Secure = true,
      Path = "/", 
      Expires = DateTime.UtcNow.AddDays(1), 
      SameSite = SameSiteMode.None
    });
    Response.Cookies.Append("user", JsonSerializer.Serialize(result.User), new CookieOptions { 
      Secure = true,
      Path = "/", 
      Expires = DateTime.UtcNow.AddDays(1), 
      SameSite = SameSiteMode.None
    });
  } 

  public AuthController(IAuthService authService) {
    _authService = authService;
  }

  [HttpPost("signup")]
  public async Task<IActionResult> SignUp([FromBody] User request) {
    if (!ModelState.IsValid) {
      return BadRequest(ModelState);
    }

    try {
      var result = await _authService.SignUp(request, request.Type == "Admin" ? UserType.Admin : UserType.User);
      
      switch (result.Item2) {
        case -2:
          return BadRequest("User with this username already exists.");
        case -1:
          return BadRequest("User with this email already exists.");
      }

      if (result.Item1 == null) {
        return StatusCode(500, "An error occurred: " + result.Item1);
      }

      SetCookies(result.Item1, Response);
      return Ok(result);
    }
    catch (Exception exception)
    {
      return StatusCode(500, "An error occurred: " + exception.Message);
    }
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] AuthenticateRequest request) {
    if (!ModelState.IsValid) {
      return BadRequest(ModelState);
    }

    var result = await _authService.Authenticate(request, request.Type == "Admin" ? UserType.Admin : UserType.User);
    
    if (result.Item2 == -2) {
      return Unauthorized("Incorrect Password.");
    }
    
    if (result.Item1 == null) {
      return NotFound("User not Found.");
    }
    
    SetCookies(result.Item1, Response);
    return Ok(result.Item1);
  }
}
