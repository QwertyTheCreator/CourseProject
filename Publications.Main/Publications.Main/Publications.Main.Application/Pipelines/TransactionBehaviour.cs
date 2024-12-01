using MediatR;
using Publications.Main.Application.Abstractions;
using Publications.Main.Application.Abstractions.Repositories;
using Publications.Main.Application.Models;

namespace Publications.Main.Application.Pipelines;

public class TransactionBehaviour<TRequest, TResponse>(IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionRequest
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.Succeeded)
        {
            try
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                Console.WriteLine("An error occurred while saving changes to database...");
                throw;
            }
        }

        return response;
    }
}
