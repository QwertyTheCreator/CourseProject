using MediatR;
using Microsoft.AspNetCore.Mvc;
using Publications.Auth.Entities;
using Publications.Auth.Entities.Enums;
using Publications.Auth.Models;
using System.Text;

namespace Publications.Auth.Controllers;

[ApiController]
[Route("/[controller]")]
public abstract class RestApiController(ISender sender) : Controller
{
    protected readonly ISender _sender = sender;

    protected IActionResult ToActionResult(Result result) =>
        result.Succeeded
        ? Ok()
        : ToErrorResult(result.Errors!);

    protected IActionResult ToActionResult<T>(Result<T> result) =>
        result.Succeeded
        ? Ok(result.Data)
        : ToErrorResult(result.Errors!);

    private IActionResult ToErrorResult(Error[] errors)
    {
        var isUnauthorized = errors.Any(x => x.Type is ErrorType.NotAuthorized);
        var isValidationFailure = errors.Any(x => x.Type is ErrorType.ValidationFailure);
        var sb = new StringBuilder();

        if (isValidationFailure)
        {
            sb.Append("Validation failure! Errors: ");
        }

        var errorMessages = string.Join(", ", errors.Select(x => x.Message));
        sb.Append(errorMessages);

        var resultMessage = sb.ToString();

        return isUnauthorized
            ? Unauthorized(resultMessage)
            : BadRequest(resultMessage);
    }

    protected async Task<IActionResult> ExecuteMediatRCommand<T>(IRequest<Result<T>> request)
    {
        var result = await _sender.Send(request, HttpContext.RequestAborted);

        return ToActionResult(result);
    }

    protected async Task<IActionResult> ExecuteMediatRCommand(IRequest<Result> request)
    {
        var result = await _sender.Send(request, HttpContext.RequestAborted);

        return ToActionResult(result);
    }
}
