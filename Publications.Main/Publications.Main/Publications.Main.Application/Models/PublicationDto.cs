using Mapster;
using Publications.Main.Domain.Entities;

namespace Publications.Main.Application.Models;

public record PublicationDto : IMapFrom<Publication>
{
    public Guid Id { get; init; }
    public string? PictureUrl { get; init; }
    public string Description { get; init; } = null!;
    public IEnumerable<Guid> UsersWhoLiked { get; init; } = [];
    public int CountOfLikes { get; init; }
    public Guid OwnerId { get; init; }

    public void ConfigureMapping(TypeAdapterConfig config) =>
        config.NewConfig<Publication, PublicationDto>()
            .Map(dest => dest.UsersWhoLiked, src => src.UsersWhoLiked.Select(x => x.Id))
            .RequireDestinationMemberSource(true);
}
