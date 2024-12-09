using Newtonsoft.Json;

namespace User.Login.Integration.Tests.Response;
public class Result<T>
{
    [JsonProperty("data")]
    public T Data { get; set; } = default;

    [JsonProperty("isSuccess")]
    public bool IsSuccess { get; set; } = false;

    [JsonProperty("error")]
    public string Error { get; set; } = string.Empty;
}
