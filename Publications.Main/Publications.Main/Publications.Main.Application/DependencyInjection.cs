using Mapster;
using Mapster.Utils;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Publications.Main.Application.Pipelines;
using System.Reflection;

namespace Publications.Main.Application;

public static class DependencyInjection
{
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
