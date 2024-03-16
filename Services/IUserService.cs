using waves_server.Models;

namespace waves_server.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> SignUp(User model);
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task<User?> AddAndUpdateUser(User userObj);
    }
}