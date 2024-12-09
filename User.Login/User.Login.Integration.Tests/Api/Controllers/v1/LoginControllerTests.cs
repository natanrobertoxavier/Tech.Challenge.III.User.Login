using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using User.Login.Communication.Response;
using User.Login.Integration.Tests.Fakes.Request;

namespace User.Login.Integration.Tests.Api.Controllers.v1;
public class LoginControllerTests() : BaseTestClient("/api/v1/login")
{
    [Fact]
    public async Task Login_ReturnsOk_WhenSuccessfulLogin()
    {
        // Arrange
        var request = new RequestLoginJsonBuilder()
            .WithEmail("natan@email.com")
            .WithPassword("123456")
            .Build();

        // Act
        var response = await Client.PostAsJsonAsync(ControllerUri, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var loggedUserJson = await DeserializeResponse<Response.Result<ResponseLoginJson>>(response);

        loggedUserJson.Data.Name.Should().NotBeNullOrWhiteSpace();
        loggedUserJson.Data.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenLoginFail_IncorrectPassword()
    {
        // Arrange
        var user = Factory.RecoverUser();

        var request = new RequestLoginJsonBuilder()
            .WithEmail(user.Email)
            .WithPassword("incorrec-password")
            .Build();

        // Act
        var response = await Client.PostAsJsonAsync(ControllerUri, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var loggedUserJson = await DeserializeResponse<ResponseLoginJson>(response);

        loggedUserJson.Name.Should().BeNull();
        loggedUserJson.Token.Should().BeNull();
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenLoginFail_IncorrectEmail()
    {
        // Arrange
        var password = Factory.RecoverPassword();

        var request = new RequestLoginJsonBuilder()
            .WithEmail("incorrec-email@email.com")
            .WithPassword(password)
            .Build();

        // Act
        var response = await Client.PostAsJsonAsync(ControllerUri, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var loggedUserJson = await DeserializeResponse<ResponseLoginJson>(response);

        loggedUserJson.Name.Should().BeNull();
        loggedUserJson.Token.Should().BeNull();
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenLoginFail_IncorrectPasswordAndEmail()
    {
        // Arrange
        var request = new RequestLoginJsonBuilder()
            .WithEmail("incorrec-email@email.com")
            .WithPassword("incorrec-password")
            .Build();

        // Act
        var response = await Client.PostAsJsonAsync(ControllerUri, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var loggedUserJson = await DeserializeResponse<ResponseLoginJson>(response);

        loggedUserJson.Name.Should().BeNull();
        loggedUserJson.Token.Should().BeNull();
    }
}
