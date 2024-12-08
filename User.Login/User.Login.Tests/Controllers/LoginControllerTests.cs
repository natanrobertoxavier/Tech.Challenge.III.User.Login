using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using User.Login.Api.Controllers.v1;
using User.Login.Application.UseCase.Login;
using User.Login.Communication.Request;
using User.Login.Communication.Response;
using User.Login.Exceptions.ExceptionBase;

namespace User.Login.Tests.Controllers;
public class LoginControllerTests
{
    private Result<ResponseLoginJson> output = new();

    [Fact]
    public async Task Login_ReturnsOkResult_WhenCredentialsAreValid()
    {
        // Arrange
        var mockUseCase = new Mock<ILoginUseCase>();

        var request = new RequestLoginJson("valid_username", "valid_password");

        var response = output.Success(new ResponseLoginJson("John Cena", "valid_token"));

        mockUseCase.Setup(useCase => useCase.LoginAsync(request))
                   .ReturnsAsync(response);

        var controller = new LoginController();

        // Act
        var result = await controller.Login(mockUseCase.Object, request) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        Assert.Equal(response, result.Value);
    }

    [Fact]
    public async Task Login_ReturnsBadRequestResult_WhenRequestIsInvalid()
    {
        // Arrange
        var mockUseCase = new Mock<ILoginUseCase>();

        var request = new RequestLoginJson(string.Empty, string.Empty);

        mockUseCase.Setup(useCase => useCase.LoginAsync(request))
            .ThrowsAsync(new ValidationErrorsException(new List<string>
            {
                "E-mail ou senha incorretos"
            }));

        var controller = new LoginController();

        // Act
        var exception = await Assert.ThrowsAsync<ValidationErrorsException>(() =>
            controller.Login(mockUseCase.Object, request));

        // Assert
        Assert.Contains("E-mail ou senha incorretos", exception.ErrorMessages);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorizedResult_WhenCredentialsAreInvalid()
    {
        // Arrange
        var mockUseCase = new Mock<ILoginUseCase>();

        var request = new RequestLoginJson("invalid_username", "invalid_password");

        mockUseCase.Setup(useCase => useCase.LoginAsync(request))
                   .ThrowsAsync(new InvalidLoginException());

        var controller = new LoginController();

        // Act
        var result = await Assert.ThrowsAsync<InvalidLoginException>(() =>
            controller.Login(mockUseCase.Object, request));

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Email ou senha incorretos", result.Message);
    }
}
