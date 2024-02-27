using Bookify.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookify.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<TRequest> logger) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var commandName = request.GetType().Name;

        try
        {
            _logger.LogInformation("Executing command {Command}", commandName);

            var result = await next();

            _logger.LogInformation("Command {Command} processed successfully", commandName);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Command {Command} processing failed", commandName);

            throw;
        }
    }
}