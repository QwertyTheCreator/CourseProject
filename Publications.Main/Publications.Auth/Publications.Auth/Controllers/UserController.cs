using MediatR;
using Microsoft.AspNetCore.Mvc;
using Publications.Auth.UseCases._User_.Commands;
using Publications.Auth.UseCases._User_.Queries;

namespace Publications.Auth.Controllers;

public class UserController(ISender sender) : RestApiController(sender)
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id) =>
       await ExecuteMediatRCommand(new GetUserQuery() { Id = id });

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody]CreateUserCommand command) =>
       await ExecuteMediatRCommand(command);
}
