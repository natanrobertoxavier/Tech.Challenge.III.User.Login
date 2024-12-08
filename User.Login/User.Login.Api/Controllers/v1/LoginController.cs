using Microsoft.AspNetCore.Mvc;
using User.Login.Application.UseCase.Login;
using User.Login.Communication.Request;
using User.Login.Communication.Response;

namespace User.Login.Api.Controllers.v1;

public class LoginController : TechChallengeController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase useCase,
        [FromBody] RequestLoginJson request)
    {
        var result = await useCase.LoginAsync(request);

        return Ok(result);
    }
}