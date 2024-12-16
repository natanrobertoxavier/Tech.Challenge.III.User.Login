namespace User.Login.Communication.Request;
public class RequestLoginJson(
    string email,
    string password)
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}
