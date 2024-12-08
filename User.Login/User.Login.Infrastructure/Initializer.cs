using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using User.Login.Domain.Extensions;
using User.Login.Domain.Services;
using User.Login.Infrastructure.RepositoryAccess;
using User.Login.Infrastructure.ServicesAccess;

namespace User.Login.Infrastructure;
public static class Initializer
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMigrator(services, configurationManager);
        AddContext(services, configurationManager);
        AddServices(services);
        RegisterServices(services, configurationManager);
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager)
    {
        _ = bool.TryParse(configurationManager.GetSection("Settings:DatabaseInMemory").Value, out bool databaseInMemory);

        if (!databaseInMemory)
        {
            services.AddFluentMigratorCore().ConfigureRunner(c =>
                 c.AddMySql5()
                  .WithGlobalConnectionString(configurationManager.GetFullConnection())
                  .ScanIn(Assembly.Load("User.Login.Infrastructure")).For.All());
        }
    }

    private static void AddContext(IServiceCollection services, IConfiguration configurationManager)
    {
        _ = bool.TryParse(configurationManager.GetSection("Settings:DatabaseInMemory").Value, out bool databaseInMemory);

        if (!databaseInMemory)
        {
            var versaoServidor = new MySqlServerVersion(new Version(8, 0, 26));
            var connectionString = configurationManager.GetFullConnection();

            services.AddDbContext<TechChallengeContext>(dbContextoOpcoes =>
            {
                dbContextoOpcoes.UseMySql(connectionString, versaoServidor);
            });
        }
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IUserQueryServiceApi, UserQueryServiceApi>();
    }

    private static void RegisterServices(IServiceCollection services, IConfiguration configurationManager)
    {
        services.AddHttpClient("UserQueryApi", client =>
        {
            client.BaseAddress = new Uri(configurationManager.GetSection("ServicesApiAddress:UserQueryApi").Value);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
    }
}
