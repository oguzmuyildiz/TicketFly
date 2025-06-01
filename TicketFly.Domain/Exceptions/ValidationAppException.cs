namespace TicketFly.Domain.Exceptions;

public class ValidationAppException(IReadOnlyDictionary<string, string[]> errors) : Exception("Validation failed")
{
    public IReadOnlyDictionary<string, string[]> Errors { get; } = errors;
}
