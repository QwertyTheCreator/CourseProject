using Microsoft.EntityFrameworkCore;
using Publications.Auth.Database;
using Publications.Auth.Database.Migrator;
using Publications.Auth.Database.Repositories;
using Publications.Auth.Database.Repositories.Abstractions;
using Publications.Auth.Database.Seeds;
using Publications.Auth.DatabasesRepositories.Abstractions;
using Publications.Main.Infrastructure.Database.Repositories;

namespace Publications.Auth.Configurations;

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

        services.AddRepositories();
        services.AddSeeds();

        services.AddScoped<IDatabseMigrator, DatabaseMigrator>();


        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }

    public static IServiceCollection AddSeeds(this IServiceCollection services)
    {
        services.AddScoped<ISeeder, RoleSeeder>();
        services.AddScoped<ISeeder, UserSeeder>();

        return services;
    }
}
