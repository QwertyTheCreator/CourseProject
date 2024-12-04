using Mapster;
using Mapster.Utils;
using MediatR;
using Publications.Auth.Configurations;
using Publications.Auth.Pipelines;
using Publications.Auth.Services;
using System.Reflection;

namespace Publications.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<InfrastructureConfiguration>(x =>
        {
            var dbString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION");

            return new InfrastructureConfiguration()
            {
                DbConnectionString = dbString ?? "Host=localhost;Port=5432;Database=Publications.Main;Username=postgres;Password=postgres"
            };
        });

        services.AddDatabase();
        services.AddSingleton<TokenService>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
        });

        services.AddMapster();
        TypeAdapterConfig.GlobalSettings.ScanInheritedTypes(typeof(DependencyInjection).Assembly);

        services.AddPermissionManagers();

        return services;
    }

    private static IServiceCollection AddPermissionManagers(this IServiceCollection services)
    {

        return services;
    }
}
