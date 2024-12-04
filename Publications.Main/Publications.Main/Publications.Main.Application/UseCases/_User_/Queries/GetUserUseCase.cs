using MapsterMapper;
using MediatR;
using Publications.Main.Application.Abstractions.Repositories;
using Publications.Main.Application.Abstractions.Services;
using Publications.Main.Application.Models;
using Publications.Main.Domain;

namespace Publications.Main.Application.UseCases._User_.Queries;

public record GetUserQuery : IRequest<Result<UserDto>>
{
    public Guid? UserId { get; init; }
}

public class GetUserUseCase(
    IUserRepository userRepository,
    IAuthService authService,
    IMapper mapper)
    : IRequestHandler<GetUserQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var tokenId = authService.UserId;
        if (!tokenId.HasValue)
        {
            return Result<UserDto>.Failure(Error.Unauthorized());
        }

        var user = request.UserId.HasValue
            ? await userRepository.GetByIdAsync(request.UserId!.Value)
            : await userRepository.GetByIdAsync(tokenId.Value);

        return user == null
            ? Result<UserDto>.Failure(Error.NotFound("Пользователь не найден"))
            : Result<UserDto>.Success(mapper.Map<UserDto>(user));
    }
}
