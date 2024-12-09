using TokenService.Manager.Controller;

namespace User.Login.Integration.Tests.Fakes;
public class PasswordEncryptorBuilder
{
    public static PasswordEncryptor Build()
    {
        return new PasswordEncryptor("%xIQ*83Y0K!@");
    }
}
