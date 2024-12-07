using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Interfaces.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Publications.Main.Application.Models;
using Publications.Main.Application.UseCases._User_.Commands;
using Publications.Main.Application.UseCases._User_.Queries;

namespace Publications.Main.WebAPI.GraphControllers;

public class UserGraphController(ISender sender) : BaseGraphController(sender)
{
    [MutationRoot("CreateUser", typeof(string))]
    public async Task<IGraphActionResult> CreateUser(CreateUserCommand command) =>
        await ExecuteGraphQuery(command, default);

    [Authorize]
    [QueryRoot("user", typeof(UserDto))]
    public async Task<IGraphActionResult> GetUser(GetUserQuery query) =>
        await ExecuteGraphQuery(query, default);
}
