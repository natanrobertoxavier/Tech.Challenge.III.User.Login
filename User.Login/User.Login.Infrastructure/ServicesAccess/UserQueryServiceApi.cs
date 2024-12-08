using Newtonsoft.Json;
using Serilog;
using System.Text;
using User.Login.Domain.RequestServices;
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

            var uri = "/api/v1/user/recover-email-password";

            var request = new UserLoginRequest(email, password);

            var jsonContent = JsonConvert.SerializeObject(request);

            var contentRequest = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, contentRequest);

            if (response.IsSuccessStatusCode)
            {
                var responseApi = DeserializeResponseObject<Result<UserResult>>(await response.Content.ReadAsStringAsync());

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
