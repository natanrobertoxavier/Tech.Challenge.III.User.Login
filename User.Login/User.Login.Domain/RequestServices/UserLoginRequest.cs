namespace User.Login.Domain.RequestServices;
public class UserLoginRequest(
    string email,
    string password)
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}
