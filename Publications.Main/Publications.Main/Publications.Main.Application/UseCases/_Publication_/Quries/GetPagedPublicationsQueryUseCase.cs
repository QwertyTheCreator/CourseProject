using MapsterMapper;
using MediatR;
using Publications.Main.Application.Abstractions.Repositories;
using Publications.Main.Application.Models;
using Publications.Main.Domain;

namespace Publications.Main.Application.UseCases._Publication_.Quries;

public record GetPagedPublicationsQuery : IRequest<Result<List<PublicationDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

public class GetPagedPublicationsQueryUseCase(
    IPublicationRepository publicationRepository,
    IMapper mapper)
    : IRequestHandler<GetPagedPublicationsQuery, Result<List<PublicationDto>>>
{
    public async Task<Result<List<PublicationDto>>> Handle(GetPagedPublicationsQuery request, CancellationToken cancellationToken)
    {
        if (request.PageNumber < 1 || request.PageSize < 1)
        {
            return Result<List<PublicationDto>>.Failure(Error.Conflict("Колличество элементов на странице и размер страницы должны быть больше одного"));
        }

        var publications = await publicationRepository.GetPaged(request.PageNumber, request.PageSize);

        return Result<List<PublicationDto>>.Success(
            publications.Select(mapper.Map<PublicationDto>).ToList());
    }
}
