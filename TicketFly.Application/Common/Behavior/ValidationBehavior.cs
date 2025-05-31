
using FluentValidation.Results;
using System.Reflection;
using TicketFly.Domain.Common;
using TicketFly.Domain.Exceptions;

namespace TicketFly.Application.Common.Behavior;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        //var context = new ValidationContext<TRequest>(request);
        //var failures = _validators
        //    .Select(v => v.Validate(context))
        //    .SelectMany(result => result.Errors)
        //    .Where(f => f is not null)
        //    .GroupBy(
        //        f => f.PropertyName.Substring(f.PropertyName.IndexOf(".") + 1),
        //        x => x.ErrorMessage, (propertyName, errorMessages) => new
        //        {
        //            Key = propertyName,
        //            Values = errorMessages.Distinct().ToArray()
        //        })
        //    .ToDictionary(x => x.Key, x => x.Values);

        //if (failures.Any())
        //    throw new ValidationAppException(failures);


        ValidationFailure[] validationFailures = await ValidateAsync(request);

        if (validationFailures.Length == 0)
        {
            return await next();
        }

        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            Type resultType = typeof(TResponse).GetGenericArguments()[0];

            MethodInfo? failureMethod = typeof(Result<>)
                .MakeGenericType(resultType)
                .GetMethod(nameof(Result<object>.ValidationFailure));

            if (failureMethod is not null)
            {
                return (TResponse)failureMethod.Invoke(
                    null,
                    [CreateValidationError(validationFailures)]);
            }
        }
        else if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(CreateValidationError(validationFailures));
        }

        return await next();
    }
    private async Task<ValidationFailure[]> ValidateAsync(TRequest request)
    {
        if (!_validators.Any())
        {
            return [];
        }

        var context = new ValidationContext<TRequest>(request);

        ValidationResult[] validationResults = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context)));

        ValidationFailure[] validationFailures = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToArray();

        return validationFailures;
    }

    private static ValidationError CreateValidationError(ValidationFailure[] validationFailures) =>
        new(validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage)).ToArray());
}

