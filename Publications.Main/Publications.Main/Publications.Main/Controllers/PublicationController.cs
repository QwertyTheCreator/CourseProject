using MediatR;
using Microsoft.AspNetCore.Mvc;
using Publications.Main.Application.UseCases._Publication_.Commands;
using Publications.Main.Application.UseCases._Publication_.Quries;

namespace Publications.Main.WebAPI.Controllers;

public class PublicationController(ISender sender) : RestApiController(sender)
{
    [HttpGet("{id:guid}")]
    public Task<IActionResult> GetPublication(Guid id) =>
        ExecuteMediatRCommand(new GetPublicationQuery() { Id = id });

    [HttpGet("paged")]
    public Task<IActionResult> GetPaged([FromQuery] GetPagedPublicationsQuery query) =>
        ExecuteMediatRCommand(query);

    [HttpPost]
    public Task<IActionResult> CreatePublication([FromBody] CreatePublicationCommand command) =>
        ExecuteMediatRCommand(command);
}
