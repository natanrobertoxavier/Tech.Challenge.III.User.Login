using User.Login.Communication.Request;

namespace User.Login.Integration.Tests.Fakes.Request;
public class RequestLoginJsonBuilder
{
    private string _email = "user@email.com";
    public string _password = "123456";

    public RequestLoginJson Build()
    {
        return new RequestLoginJson(_email, _password);
    }

    public RequestLoginJsonBuilder WithEmail(string value)
    {
        _email = value;
        return this;
    }

    public RequestLoginJsonBuilder WithPassword(string value)
    {
        _password = value;
        return this;
    }
}