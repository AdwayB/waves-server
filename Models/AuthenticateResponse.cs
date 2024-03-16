namespace waves_server.Models;

public class AuthenticateResponse
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }


    public AuthenticateResponse(User user, string token)
    {
        UserId = user.UserId;
        Username = user.Username;
        Token = token;
    }
}