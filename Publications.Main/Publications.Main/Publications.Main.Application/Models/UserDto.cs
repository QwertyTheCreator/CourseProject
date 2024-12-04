using Publications.Main.Domain.Entities;

namespace Publications.Main.Application.Models;

public record UserDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? PictureUrl { get; init; }
    public ICollection<Publication> Publications { get; init; } = [];
    public ICollection<Publication> Favourites { get; init; } = [];
}
