namespace TicketFly.Domain.Events;

public class ClientCreatedEvent(Client item) : BaseEvent
{
    public Client Item { get; } = item;
}
