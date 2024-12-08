using Serilog;
using TokenService.Manager.Controller;

namespace User.Login.Api;

public static class Initializer
{
    public static void AddTokenManager(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddJWTToken(services, configuration);
        AddSerilog(services);
    }

    private static void AddJWTToken(IServiceCollection services, IConfiguration configuration)
    {
        var sectionLifeTime = configuration.GetRequiredSection("Settings:Jwt:LifeTimeTokenMinutes");
        var sectionKey = configuration.GetRequiredSection("Settings:Jwt:KeyToken");
        services.AddScoped(option => new TokenController(int.Parse(sectionLifeTime.Value), sectionKey.Value));
    }

    private static void AddSerilog(IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        services.AddSingleton(Log.Logger);
    }
}
