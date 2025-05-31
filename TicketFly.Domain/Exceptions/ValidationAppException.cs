﻿namespace TicketFly.Domain.Exceptions;

public class ValidationAppException : Exception
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }
    public ValidationAppException(IReadOnlyDictionary<string, string[]> errors)
        : base("Validation failed") => Errors = errors;
    
}
