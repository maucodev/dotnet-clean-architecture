using Bookify.Application.Abstractions.Messaging;
using FluentValidation;
using MediatR;
using Bookify.Application.Exceptions;

namespace Bookify.Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => validationResult.Errors.Count != 0)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                new PropertyName(validationFailure.PropertyName),
                new ErrorMessage(validationFailure.ErrorMessage)))
            .ToList();

        if (validationErrors.Count != 0)
        {
            throw new Exceptions.ValidationException(validationErrors);
        }

        return await next();
    }
}