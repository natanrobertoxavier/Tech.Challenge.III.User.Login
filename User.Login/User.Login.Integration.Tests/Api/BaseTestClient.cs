using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using User.Login.Integration.Tests.Fakes;

namespace User.Login.Integration.Tests.Api;
public abstract class BaseTestClient
{
    protected readonly HttpClient Client;
    protected readonly CustomWebApplicationFactory<Program> Factory;
    protected readonly string ControllerUri;

    protected BaseTestClient(
        string controllerUri)
    {
        ControllerUri = controllerUri;
        Factory = new CustomWebApplicationFactory<Program>();
        Client = Factory.CreateClient(new WebApplicationFactoryClientOptions());
    }

    protected static async Task<T> DeserializeResponse<T>(HttpResponseMessage response) =>
        JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
}
