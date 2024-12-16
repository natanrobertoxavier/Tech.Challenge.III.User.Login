using Moq;
using Serilog;
using TokenService.Manager.Controller;
using User.Login.Application.UseCase.Login;
using User.Login.Communication.Request;
using User.Login.Domain.ResultServices;
using User.Login.Domain.Services;
using User.Login.Exceptions;
using User.Login.Exceptions.ExceptionBase;

namespace User.Login.Tests.UseCase;
public class LoginUseCaseTests
{
    private readonly Mock<IUserQueryServiceApi> _mockUserQueryServiceApi;
    private readonly Mock<ILogger> _mockLogger;
    private readonly TokenController _mockTokenController;
    private readonly PasswordEncryptor _passwordEncryptor;
    private readonly LoginUseCase _useCase;
    private const string additionalKey = "123456";
    private const string keyToken = "V1lYOTEyZ0doMVZDc0cyWTY2SFgjVHBVNFozZVpG";
    private Result<UserResult> output = new Result<UserResult>();

    public LoginUseCaseTests()
    {
        _mockUserQueryServiceApi = new Mock<IUserQueryServiceApi>();
        _mockLogger = new Mock<ILogger>();
        _mockTokenController = new TokenController(1000, keyToken);
        _passwordEncryptor = new PasswordEncryptor(additionalKey);

        _useCase = new LoginUseCase(
            _mockUserQueryServiceApi.Object,
            _passwordEncryptor,
            _mockTokenController,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task Execute_ShouldReturnResponseLoginJson_WhenCredentialsAreValid()
    {
        // Arrange
        var request = new RequestLoginJson("email@example.com", "password123");

        var encryptedPassword = _passwordEncryptor.Encrypt(request.Password);

        var user = new UserResult()
        {
            Id = Guid.NewGuid(),
            RegistrationDate = DateTime.UtcNow,
            Name = "John Cena",
            Email = "user@example.com",
            Password = encryptedPassword
        };

        var expectedUser = output.Success(user);

        _mockUserQueryServiceApi
            .Setup(r => r.RecoverByEmailAndPasswordAsync(request.Email, encryptedPassword))
            .ReturnsAsync(expectedUser);

        // Act
        var result = await _useCase.LoginAsync(request);

        // Assert
        Assert.Equal(user.Name, result.Data.Name);
        _mockUserQueryServiceApi.Verify(r => r.RecoverByEmailAndPasswordAsync(request.Email, encryptedPassword), Times.Once);
    }

    [Fact]
    public async Task Execute_ShouldThrowInvalidLoginException_WhenCredentialsAreInvalid()
    {
        // Arrange
        var request = new RequestLoginJson("email@example.com", "invalidpassword");

        var encryptedPassword = _passwordEncryptor.Encrypt(request.Password);

        var expectedUser = output.Failure("Email ou senha inválidos.");

        _mockUserQueryServiceApi
            .Setup(r => r.RecoverByEmailAndPasswordAsync(request.Email, encryptedPassword))
            .ReturnsAsync(expectedUser);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidLoginException>(() => _useCase.LoginAsync(request));

        Assert.NotNull(exception);
        Assert.Equal(ErrorsMessages.InvalidLogin, exception.Message);
        _mockUserQueryServiceApi.Verify(r => r.RecoverByEmailAndPasswordAsync(request.Email, encryptedPassword), Times.Once);
    }
}
