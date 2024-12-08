using User.Login.Domain.ResultServices;

namespace User.Login.Domain.Services;
public interface IUserQueryServiceApi
{
    Task<Result<UserResult>> RecoverByEmailAndPasswordAsync(string email, string password);
}
