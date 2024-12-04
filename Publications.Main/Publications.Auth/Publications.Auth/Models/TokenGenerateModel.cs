namespace Publications.Auth.Models;

public record TokenGenerateModel
{
    public string Username { get; set; } = null!;
    public Guid Id { get; set; }
    public string RoleName { get; set; } = null!;
}
