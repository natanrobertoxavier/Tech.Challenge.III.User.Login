using User.Login.Communication.Request;
using User.Login.Communication.Response;

namespace User.Login.Application.UseCase.Login;
public interface ILoginUseCase
{
    Task<Result<ResponseLoginJson>> LoginAsync(RequestLoginJson request);
}
