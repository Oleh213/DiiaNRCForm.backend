using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DiiaNRCForm.Infrastructure.MediatR;

internal class ValidatingCommandPreProcessor<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    where TCommand : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;
    private readonly ILogger<ValidatingCommandPreProcessor<TCommand, TResponse>> _logger;

    public ValidatingCommandPreProcessor(IEnumerable<IValidator<TCommand>> validators,
        ILogger<ValidatingCommandPreProcessor<TCommand, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }


    public async Task<TResponse> Handle(TCommand request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        foreach (var validator in _validators)
        {
            var result = await validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }

        return await next();
    }
}
