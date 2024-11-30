namespace Publications.Main.Common;

public interface IDatabseMigrator
{
    void Migrate();

    void ApplySeeds();
}
