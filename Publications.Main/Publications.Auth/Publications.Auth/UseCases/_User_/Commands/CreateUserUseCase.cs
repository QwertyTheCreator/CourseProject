using MediatR;
using Publications.Auth.Database.Repositories.Abstractions;
using Publications.Auth.DatabasesRepositories.Abstractions;
using Publications.Auth.Entities;
using Publications.Auth.Models;
using Publications.Auth.Pipelines;
using Publications.Auth.Services;

namespace Publications.Auth.UseCases._User_.Commands;

public record CreateUserCommand : IRequest<Result<string>>, ITransactionRequest
{
    public Guid Id { get; init; }
    public string Login { get; init; } = null!;
    public string Password { get; init; } = null!;
}


public class CreateUserUseCase(
    IUserRepository userRepository,
    TokenService tokenService,
    IRoleRepository roleRepository)
    : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.IsExistsAsync(x => x.Login == request.Login || x.Id == request.Id))
        {
            return Result<string>.Failure(Error.Conflict("Такой логин уже занят"));
        }

        var role = await roleRepository.GetDefaultRole();

        var user = new User()
        {
            Id = request.Id,
            Login = request.Login,
            PasswordHash = HashService.HashPassword(request.Password),
            Role = role,
            RoleId = role.Id
        };

        userRepository.Create(user);

        var tokenGenModel = new TokenGenerateModel
        { Id = user.Id, RoleName = user.Role.Title, Username = user.Login };

        var jwt = tokenService.GenerateToken(tokenGenModel);

        return Result<string>.Success(jwt);
    }
}
