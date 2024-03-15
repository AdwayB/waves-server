using Microsoft.AspNetCore.Mvc;
using waves_server.Models;
using waves_server.Services;

namespace waves_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var authResult = _authService.Authenticate(model.Username, model.Password);

            if (authResult == null)
                return Unauthorized();

            return Ok(authResult);
        }
    }
}