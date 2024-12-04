using Publications.Auth.Database.Migrator;

namespace Publications.Auth.Configurations;

public static class WebApplicationExtensions
{
    public static void ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var publicMigrator = scope.ServiceProvider.GetService<IDatabseMigrator>();
        publicMigrator!.Migrate();

        publicMigrator.ApplySeeds();
    }
}
