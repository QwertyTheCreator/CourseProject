using Publications.Main.Domain.Entities.Abstractions;

namespace Publications.Main.Domain.Entities;

public class User : BaseEntity, IPicture
{
    public string Name { get; set; } = null!;
    public string? PictureUrl { get; set; }
    public ICollection<Publication> Publications { get; set; } = [];
    public ICollection<Publication> Favourites { get; set; } = [];
}
