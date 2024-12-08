namespace User.Login.Communication.Response;

public struct Result<T>(
    T value,
    bool isSuccess,
    string error)
{
    public T Data { get; } = value;
    public bool IsSuccess { get; } = isSuccess;
    public string Error { get; } = error;

    public Result<T> Success(T data) => new Result<T>(data, true, string.Empty);

    public new Result<T> Failure(string error) => new Result<T>(default, false, error);
}
