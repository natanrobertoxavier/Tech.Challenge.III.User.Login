using System.Text.Json;
using System.Text.Json.Serialization;

namespace User.Login.Domain.ResultServices;
public abstract class Base
{
    protected T DeserializeResponseObject<T>(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        options.Converters.Add(new JsonStringEnumConverter());
        return JsonSerializer.Deserialize<T>(json, options);
    }
}
