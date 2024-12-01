using MapsterMapper;
using MediatR;
using Publications.Main.Application.Abstractions;
using Publications.Main.Application.Abstractions.Repositories;
using Publications.Main.Application.Abstractions.Services;
using Publications.Main.Application.Models;
using Publications.Main.Domain.Entities;

namespace Publications.Main.Application.UseCases._Publication_.Commands;

public record CreatePublicationCommand : IRequest<Result<PublicationDto>>, ITransactionRequest
{
    public string? PictureUrl { get; init; }
    public string Description { get; init; } = null!;
}

public class CreatePublicationCommandUseCase(
    IPublicationRepository publicationRepository,
    IAuthService authService,
    IMapper mapper)
    : IRequestHandler<CreatePublicationCommand, Result<PublicationDto>>
{
    public Task<Result<PublicationDto>> Handle(CreatePublicationCommand request, CancellationToken cancellationToken)
    {
        var userId = authService.UserId;
        if (!userId.HasValue)
        {
            return Task.FromResult(Result<PublicationDto>.Failure(Domain.Error.Unauthorized()));
        }

        var publication = new Publication()
        {
            OwnerId = userId.Value,
            Description = request.Description,
            PictureUrl = request.PictureUrl,
            UsersWhoLiked = []
        };

        publicationRepository.Create(publication);

        return Task.FromResult(Result<PublicationDto>.Success(mapper.Map<PublicationDto>(publication)));
    }
}
