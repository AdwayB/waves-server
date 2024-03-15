using waves_server.Models;

namespace waves_server.Services;

public interface IAuthService
{
    AuthResponse? Authenticate(string username, string password);
}