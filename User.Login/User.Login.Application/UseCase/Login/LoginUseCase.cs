﻿using Serilog;
using TokenService.Manager.Controller;
using User.Login.Communication.Request;
using User.Login.Communication.Response;
using User.Login.Domain.Services;
using User.Login.Exceptions.ExceptionBase;

namespace User.Login.Application.UseCase.Login;
public class LoginUseCase(
    IUserQueryServiceApi userQueryServiceApi,
    PasswordEncryptor passwordEncryptor,
    TokenController tokenController,
    ILogger logger) : ILoginUseCase
{
    private readonly IUserQueryServiceApi _userQueryServiceApi = userQueryServiceApi;
    private readonly PasswordEncryptor _passwordEncryptor = passwordEncryptor;
    private readonly TokenController _tokenController = tokenController;
    private readonly ILogger _logger = logger;

    public async Task<Result<ResponseLoginJson>> LoginAsync(RequestLoginJson request)
    {
        var output = new Result<ResponseLoginJson>();
        try
        {
            _logger.Information($"Start {nameof(LoginAsync)}. User: {request.Email}.");

            var encryptedPassword = _passwordEncryptor.Encrypt(request.Password);

            var user = await _userQueryServiceApi.RecoverByEmailAndPasswordAsync(request.Email, encryptedPassword) ??
                throw new InvalidLoginException();

            _logger.Information($"End {nameof(LoginAsync)}. User: {request.Email}.");

            return output.Success(new ResponseLoginJson(user.Data.Name, _tokenController.GenerateToken(user.Data.Email)));
        }
        catch (InvalidLoginException ex)
        {
            var errorMessage = string.Format("E-mail ou senha inválidos!");

            _logger.Error(ex, errorMessage);

            throw;
        }
        catch (Exception ex)
        {
            var errorMessage = string.Format("There are an error: {0}", ex.Message);

            _logger.Error(ex, errorMessage);

            return output.Failure(errorMessage);
        }
    }
}