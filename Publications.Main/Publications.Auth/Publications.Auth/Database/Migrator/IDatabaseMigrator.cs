namespace Publications.Auth.Database.Migrator;

public interface IDatabseMigrator
{
    void Migrate();

    void ApplySeeds();
}