﻿using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Publications.Auth.Database.Seeds;
namespace Publications.Auth.Database.Migrator;

public class DatabaseMigrator(AppDbContext dbContext, ILogger<DatabaseMigrator> logger, IEnumerable<ISeeder> seeders)
    : IDatabseMigrator
{
    public void Migrate()
    {
        try
        {
            dbContext.Database.BeginTransaction();
            dbContext.Database.Migrate();
            dbContext.Database.CommitTransaction();
            logger.LogInformation("Database has been successfully migrated...");
        }
        catch (Exception e)
        {
            dbContext.Database.RollbackTransaction();

            logger.LogCritical(e, "Exception occured while database migration:\r\n{message}", e.Message);
            throw;
        }
    }

    public void ApplySeeds()
    {
        foreach (var seeder in seeders)
        {
            seeder.Seed();
        }
    }
}
