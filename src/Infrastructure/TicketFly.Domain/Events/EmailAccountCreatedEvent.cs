namespace TicketFly.Domain.Events;

public class EmailAccountCreatedEvent(EmailAccount item) : BaseEvent
{
    public EmailAccount Item { get; } = item;
}
