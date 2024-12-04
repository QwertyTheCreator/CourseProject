using MediatR;
using Publications.Main.Application.Abstractions;
using Publications.Main.Application.Abstractions.Repositories;
using Publications.Main.Application.Abstractions.Services;
using Publications.Main.Application.Models;
using Publications.Main.Application.Models.AuthApi;
using Publications.Main.Domain;
using Publications.Main.Domain.Entities;

namespace Publications.Main.Application.UseCases._User_.Commands;

public record CreateUserCommand : IRequest<Result<string>>, ITransactionRequest
{
    public string Login { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string? PictureUrl { get; set; }
}

public class CreateUserUseCase(
    IRequestSender requestSender,
    IUserRepository userRepository)
    : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var createUserDto = new CreateUserDto()
        {
            Id = id,
            Login = request.Login,
            Password = request.Password,
        };

        var createResult = await requestSender.CreateUser(createUserDto);
        if (createResult.Failed)
        {
            return Result<string>.Failure(Error.Unauthorized());
        }

        var user = new User()
        {
            Id = id,
            Name = request.Login,
            PictureUrl = request.PictureUrl
        };

        userRepository.Create(user);

        return Result<string>.Success(createResult.Data!);
    }
}
