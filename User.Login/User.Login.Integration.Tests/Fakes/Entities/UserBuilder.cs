namespace User.Login.Integration.Tests.Fakes.Entities;
public class UserBuilder
{
    public static (Domain.Entities.User user, string password) Build()
    {
        var password = "123456";
        var id = Guid.NewGuid();

        var builtUser = new Domain.Entities.User(
            id,
            DateTime.UtcNow,
            "John Cena",
            $"{id.ToString()[..6]}@email.com",
            PasswordEncryptorBuilder.Build().Encrypt(password)
        );

        return (builtUser, password);
    }
}