using Publications.Main.Domain.Entities.Abstractions;

namespace Publications.Main.Domain.Entities;

public class Publication : BaseEntity, IPicture
{
    public string? PictureUrl { get; set; }
    public string Description { get; set; } = null!;
    public ICollection<User> UsersWhoLiked { get; set; } = [];
    public int CountOfLikes => UsersWhoLiked.Count;
    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = null!;
}
