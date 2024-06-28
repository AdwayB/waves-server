using waves_server.Models;

namespace waves_server.Services
{
    public interface IAuthService {
        Task<(AuthenticateResponse?, int)> SignUp (User model, UserType userType);
        Task<(AuthenticateResponse?, int)> Authenticate (AuthenticateRequest model, UserType userType);
    }
}