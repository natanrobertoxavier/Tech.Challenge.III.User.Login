using User.Login.Infrastructure.RepositoryAccess;
using User.Login.Integration.Tests.Fakes.Entities;

namespace User.Login.Integration.Tests.Fakes;
public class ContextSeedInMemory
{
    public static (Domain.Entities.User user, string password) Seed(TechChallengeContext context)
    {
        (var user, string password) = UserBuilder.Build();

        context.Users.Add(user);
        context.SaveChanges();

        return (user, password);
    }
}
