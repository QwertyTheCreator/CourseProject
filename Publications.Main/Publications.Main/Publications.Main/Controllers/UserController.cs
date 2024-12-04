using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publications.Main.Application.UseCases._User_.Commands;
using Publications.Main.Application.UseCases._User_.Queries;

namespace Publications.Main.WebAPI.Controllers;

public class UserController(ISender sender) : RestApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command) =>
        await ExecuteMediatRCommand(command);

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id) =>
        await ExecuteMediatRCommand(new GetUserQuery() { UserId = id });
}
