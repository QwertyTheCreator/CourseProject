using GraphQL.AspNet.Controllers;
using GraphQL.AspNet.Interfaces.Controllers;
using MediatR;
using Publications.Main.Application.Models;

namespace Publications.Main.WebAPI.GraphControllers;

public class BaseGraphController(ISender sender) : GraphController
{
    protected readonly ISender _sender = sender;

    protected IGraphActionResult ToGraphResult(Result result)
    {
        if (result.Succeeded)
        {
            return Ok(new EmptyResponse(result.Succeeded));
        }

        var firstError = result.Errors.First();
        return Error(firstError.Message, firstError.Type.ToString());
    }

    protected IGraphActionResult ToGraphResult<T>(Result<T> result) where T : class
    {
        if (result.Succeeded)
        {
            return Ok(result.Data);
        }

        var firstError = result.Errors.First();
        return Error(firstError.Message, firstError.Type.ToString());
    }

    protected async Task<IGraphActionResult> ExecuteGraphQuery(
        IRequest<Result> request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);

        return ToGraphResult(result);
    }

    protected async Task<IGraphActionResult> ExecuteGraphQuery<T>(
        IRequest<Result<T>> request,
        CancellationToken cancellationToken)
        where T : class
    {
        var result = await _sender.Send(request, cancellationToken);

        return ToGraphResult(result);
    }
}
