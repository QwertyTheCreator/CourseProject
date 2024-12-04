using Microsoft.Extensions.DependencyInjection;
using Publications.Main.Application.Abstractions.Services;
using Publications.Main.Application.Utils;
using Publications.Main.Infrastructure.Configuration;
using Publications.Main.Infrastructure.Services;

namespace Publications.Main.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<InfrastructureConfiguration>(x =>
        {
            var dbString = EnvFetcher.GetEnvVariable("DATABASE_CONNECTION");
            var authUrl = EnvFetcher.GetEnvVariable("AUTH_URL");

            return new InfrastructureConfiguration()
            {
                DbConnectionString = dbString ?? "Host=localhost;Port=5432;Database=Publications.Main;Username=postgres;Password=postgres",
                AuthServiceUrl = authUrl ?? "https://localhost:7222/"
            };
        });

        services.AddDatabase();
        services.AddScoped<IAuthService, AuthService>();
        services.AddSingleton<IAuthAPI, AuthAPI>();
        services.AddHttpClient();
        services.AddSingleton<IRequestSender, RequestSender>();

        return services;
    }
}
