using Mapster;
using Mapster.Utils;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Publications.Main.Application.Pipelines;
using Publications.Main.Domain.Constants;
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

        services.AddAuth();
        services.AddPermissionManagers();

        return services;
    }

    private static IServiceCollection AddPermissionManagers(this IServiceCollection services)
    {

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = JwtOptions.ISSUER,
                     ValidAudience = JwtOptions.AUDIENCE,
                     IssuerSigningKey = JwtOptions.GetSymmetricSecurityKey()
                 });

        return services;
    }
}
