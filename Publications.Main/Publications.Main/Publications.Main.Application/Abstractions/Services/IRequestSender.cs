using Publications.Main.Application.Models.AuthApi;
using Publications.Main.Application.Models;

namespace Publications.Main.Application.Abstractions.Services;

public interface IRequestSender
{
    public Task<Result<string>> CreateUser(CreateUserDto createUserDto);
}
