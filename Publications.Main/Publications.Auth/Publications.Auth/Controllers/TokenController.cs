using MediatR;
using Microsoft.AspNetCore.Mvc;
using Publications.Auth.UseCases._Token_;

namespace Publications.Auth.Controllers;

public class TokenController(ISender sender) : RestApiController(sender)
{
    [HttpPost("login")]
    public async Task<IActionResult> GenerateToken([FromBody]LoginCommand command) =>
        await ExecuteMediatRCommand(command);
}
