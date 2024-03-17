using Microsoft.AspNetCore.Mvc;
using waves_server.Models;
using waves_server.Services;

namespace waves_server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase {
  private readonly IUserService _userService;

  public UserController(IUserService userService) {
    _userService = userService;
  }

  [HttpPost("signup")]
  public async Task<IActionResult> SignUp([FromBody] User request) {
    if (!ModelState.IsValid) {
      return BadRequest(ModelState);
    }

    try {
      var result = await _userService.SignUp(request, request.Type == "Admin" ? UserType.Admin : UserType.User);
      
      if (result == null) {
        return BadRequest("Unable to create user.");
      }

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

    var response = await _userService.Authenticate(request, request.Type == "Admin" ? UserType.Admin : UserType.User);
    if (response == null) {
      return Unauthorized("Username or password is incorrect.");
    }

    return Ok(response);
  }
}
