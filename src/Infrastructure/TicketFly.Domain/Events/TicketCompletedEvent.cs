namespace TicketFly.Domain.Events;

public class TicketCompletedEvent(Ticket item) : BaseEvent
{
    public Ticket Item { get; } = item;
}
