namespace User.Login.Communication.Response;
public class ResponseLoginJson(
    string name,
    string token)
{
    public string Name { get; set; } = name;
    public string Token { get; set; } = token;
}
