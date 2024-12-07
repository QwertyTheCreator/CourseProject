using Mapster;
using Publications.Main.Domain.Entities;

namespace Publications.Main.Application.Models;

public record UserDto : IMapFrom<User>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? PictureUrl { get; init; }
    public ICollection<PublicationDto> Publications { get; init; } = [];

    public void ConfigureMapping(TypeAdapterConfig config) =>
        config.NewConfig<User, UserDto>()
            //.Map(dest => dest.Publications, src => src.Publications.Select(x => x.Id))
            .RequireDestinationMemberSource(true);
}
