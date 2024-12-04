using MediatR;
using Publications.Auth.Database.Repositories.Abstractions;
using Publications.Auth.Entities;
using Publications.Auth.Models;
using Publications.Auth.Services;

namespace Publications.Auth.UseCases._Token_;

public record LoginCommand : IRequest<Result<string>>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginUseCase(
    IUserRepository userRepository,
    TokenService tokenService) 
    : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsername(request.Login);
        if(user == null)
        {
            return Result<string>.Failure(Error.NotFound("Неправильный логин или пароль"));
        }

        var hashedPassword = HashService.HashPassword(request.Password);
        if(hashedPassword != user.PasswordHash)
        {
            return Result<string>.Failure(Error.NotFound("Неправильный логин или пароль"));
        }

        var tokenModel = new TokenGenerateModel()
        {
            Id = user.Id,
            RoleName = user.Role.Title,
            Username = user.Login
        };

        var jwt = tokenService.GenerateToken(tokenModel);

        return Result<string>.Success(jwt);
    }
}
