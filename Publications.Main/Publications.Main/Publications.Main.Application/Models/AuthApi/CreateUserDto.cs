using Newtonsoft.Json;

namespace Publications.Main.Application.Models.AuthApi;

public record CreateUserDto
{
    [JsonProperty("login")] public string Login { get; init; } = null!;
    [JsonProperty("password")] public string Password { get; init; } = null!;
    [JsonProperty("id")] public Guid Id { get; init; }
}
