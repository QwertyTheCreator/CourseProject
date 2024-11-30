namespace Publications.Main.Application.Utils;

public static class EnvFetcher
{
    public static string? GetEnvVariable(string name) =>
        Environment.GetEnvironmentVariable(name);
}
