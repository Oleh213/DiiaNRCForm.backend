using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DiiaNRCForm.Infrastructure.MediatR;

internal class CommandLoggingProcessor<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    where TCommand : IRequest<TResponse>
{
    private readonly ILogger<CommandLoggingProcessor<TCommand, TResponse>> _logger;


    public CommandLoggingProcessor(ILogger<CommandLoggingProcessor<TCommand, TResponse>> logger)
    {
        _logger = logger;
    }


    public async Task<TResponse> Handle(TCommand request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var commandTypeName = request.GetType().Name;
        _logger.LogInformation($"Handling command '{commandTypeName}'");

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var response = await next();

        stopwatch.Stop();

        _logger.LogInformation($"Command '{commandTypeName}' took {stopwatch.ElapsedMilliseconds}ms to complete and returned a response of type '{response?.GetType().Name ?? "<unknown>"}'");

        return response;
    }
}
