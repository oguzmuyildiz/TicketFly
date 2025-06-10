using MassTransit;

namespace RabbitMQ.Shared.Models.TicketApi.Events;

[EntityName("emailparser.ticketapi.ticketcreatedevent")]
public record TicketCreatedEvent(
    Guid OrganizationId,
    string Host,
    string UserName,
    string ApiKey,
    int Port)
{
    public Guid OrganizationId { get; private set; } = OrganizationId;
    public string Host { get; private set; } = Host;
    public string UserName { get; private set; } = UserName;
    public string ApiKey { get; private set; } = ApiKey;
    public int Port { get; private set; } = Port;
}