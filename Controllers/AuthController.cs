﻿using Microsoft.AspNetCore.Mvc;
using waves_server.Models;
using waves_server.Services;

namespace waves_server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase {
  private readonly IAuthService _authService;
  private static void SetCookies (AuthenticateResponse result, HttpResponse Response) {
    var cookieOptions = new CookieOptions { 
      HttpOnly = true,
      Secure = true,
      Path = "/", 
      Expires = DateTime.UtcNow.AddDays(1), 
      SameSite = SameSiteMode.None
    };
      
    Response.Cookies.Append("jwt", result.Token, cookieOptions);
    Response.Cookies.Append("userType", result.Type, cookieOptions);
    Response.Cookies.Append("userId", result.UserId.ToString(), cookieOptions);
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
      
      if (result == null) {
        return BadRequest("Unable to create user.");
      }

      SetCookies(result, Response);
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
    if (result == null) {
      return Unauthorized("Username or password is incorrect.");
    }
    SetCookies(result, Response);
    return Ok(result);
  }
}
