using Microsoft.Extensions.DependencyInjection;
using Publications.Main.Application.Utils;
using Publications.Main.Infrastructure.Configuration;

namespace Publications.Main.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<InfrastructureConfiguration>(x =>
        {
            var dbString = EnvFetcher.GetEnvVariable("DATABASE_CONNECTION");

            return new InfrastructureConfiguration()
            {
                DbConnectionString = dbString ?? "Host=localhost;Port=5432;Database=Publications.Main;Username=postgres;Password=postgres"
            };
        });

        services.AddDatabase();

        return services;
    }
}
