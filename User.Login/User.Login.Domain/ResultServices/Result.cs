using Newtonsoft.Json;

namespace User.Login.Domain.ResultServices;
public class Result<T>
{
    [JsonProperty("data")]
    public T Data { get; set; } = default;

    [JsonProperty("isSuccess")]
    public bool IsSuccess { get; set; } = false;

    [JsonProperty("error")]
    public string Error { get; set; } = string.Empty;

    public Result() { }

    public Result(
        T data,
        bool isSuccess,
        string error)
    {
        Data = data;
        IsSuccess = isSuccess;
        Error = error;
    }

    public Result<T> Success(T data) => new Result<T>(data, true, string.Empty);

    public new Result<T> Failure(string error) => new Result<T>(default, false, error);
}
