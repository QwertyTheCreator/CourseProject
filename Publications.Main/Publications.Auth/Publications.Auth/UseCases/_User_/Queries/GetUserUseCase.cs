using MapsterMapper;
using MediatR;
using Publications.Auth.Database.Repositories.Abstractions;
using Publications.Auth.Entities;
using Publications.Auth.Models;

namespace Publications.Auth.UseCases._User_.Queries;

public record GetUserQuery : IRequest<Result<UserDto>>
{
    public Guid Id { get; set; }
}


public class GetUserUseCase(
    IUserRepository userRepo,
    IMapper mapper)
    : IRequestHandler<GetUserQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepo.GetByIdAsync(request.Id);
        if (user == null)
        {
            return Result<UserDto>.Failure(Error.NotFound("Пользователь не найден"));
        }

        return Result<UserDto>.Success(mapper.Map<UserDto>(user));
    }
}
