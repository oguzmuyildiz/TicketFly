namespace TicketFly.Domain.Exceptions;

[Serializable]
public class ProblemException(string error, string message) : Exception(message)
{
    public string Error { get; set; } = error;
    public string Message { get; set; } = message;
}