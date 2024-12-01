using MapsterMapper;
using MediatR;
using Publications.Main.Application.Abstractions.Repositories;
using Publications.Main.Application.Models;
using Publications.Main.Domain;

namespace Publications.Main.Application.UseCases._Publication_.Quries;

public record GetPublicationQuery : IRequest<Result<PublicationDto>>
{
    public Guid Id { get; init; }
}

public class GetPublicationQueryUseCase(
    IPublicationRepository publicationRepository,
    IMapper mapper)
    : IRequestHandler<GetPublicationQuery, Result<PublicationDto>>
{
    public async Task<Result<PublicationDto>> Handle(GetPublicationQuery request, CancellationToken cancellationToken)
    {
        var publication = await publicationRepository.GetByIdAsync(request.Id);

        return publication == null
            ? Result<PublicationDto>.Failure(Error.NotFound("Публикация не найдена"))
            : Result<PublicationDto>.Success(mapper.Map<PublicationDto>(publication));
    }
}
