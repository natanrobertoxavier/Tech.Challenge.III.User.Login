using Serilog;
using User.Login.Domain.ResultServices;
using User.Login.Domain.Services;

namespace User.Login.Infrastructure.ServicesAccess;
public class UserQueryServiceApi(
    IHttpClientFactory httpClientFactory,
    ILogger logger) : Base, IUserQueryServiceApi
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger _logger = logger;

    public async Task<Result<UserResult>> RecoverByEmailAndPasswordAsync(string email, string password)
    {
        _logger.Information($"{nameof(RecoverByEmailAndPasswordAsync)} - Initiating call to User.Query Api. User: {email}.");

        var output = new Result<UserResult>();

        try
        {
            var client = _httpClientFactory.CreateClient("UserQueryApi");

            var uri = string.Format("/api/v1/user/{0}", email);

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responseApi = DeserializeResponseObject<Result<UserResult>>(content);

                _logger.Information($"{nameof(RecoverByEmailAndPasswordAsync)} - Ended call to User.Query Api. User: {email}.");

                return responseApi;
            }

            var failMessage = $"{nameof(RecoverByEmailAndPasswordAsync)} - An error occurred when calling the Users.Query Api. StatusCode: {response.StatusCode}";

            _logger.Error(failMessage);

            return output.Failure(failMessage);
        }
        catch (Exception ex)
        {
            var errorMessage = $"{nameof(RecoverByEmailAndPasswordAsync)} - An error occurred when calling the Users.Query Api. Error: {ex.Message}";

            _logger.Error(errorMessage);

            return output.Failure(errorMessage);
        }
    }
}
