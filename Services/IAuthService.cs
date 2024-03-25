using waves_server.Models;

namespace waves_server.Services
{
    public interface IAuthService {
        Task<AuthenticateResponse?> SignUp (User model, UserType userType);
        Task<AuthenticateResponse?> Authenticate (AuthenticateRequest model, UserType userType);
    }
}