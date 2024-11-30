using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Publications.Main.Application.Abstractions.Repositories;
using Publications.Main.Common;
using Publications.Main.Infrastructure.Database;
using Publications.Main.Infrastructure.Database.Repositories;

namespace Publications.Main.Infrastructure.Configuration;

public static class DatabseConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<IUnitOfWork, AppDbContext>((serviceProvider, opt) =>
        {
            var x = serviceProvider
                .GetRequiredService<InfrastructureConfiguration>();

            opt.UseNpgsql(x.DbConnectionString);
            opt.EnableSensitiveDataLogging();
            opt.EnableDetailedErrors();
        });

        services.AddScoped<IDatabseMigrator, DatabaseMigrator>();
        services.AddRepositories();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPublicationRepository, PublicationRepository>();

        return services;
    }
}
