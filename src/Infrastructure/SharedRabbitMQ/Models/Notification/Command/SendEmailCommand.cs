using MassTransit;

namespace TicketFly.Shared.Models.Notification.Command;

[EntityName("notification.sendemailcommand")]
public record SendEmailCommand(string To, string Subject, string Body)
{
    public string To { get; protected set; } = To;
    public string Subject { get; protected set; } = Subject;
    public string Body { get; protected set; } = Body;
}
