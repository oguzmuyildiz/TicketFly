using MassTransit;

namespace TicketFly.Shared.Models.EmailParser.Events;

[EntityName("emailparser.newemailreceivedevent")]
public record NewEmailReceivedEvent(
    Guid OrganizationId,
    string From,
    string Subject,
    string Body,
    string To)
{
    public Guid OrganizationId { get; private set; } = OrganizationId;
    public string From { get; private set; } = From;
    public string Subject { get; private set; } = Subject;
    public string Body { get; private set; } = Body;
    public string To { get; private set; } = To;
}