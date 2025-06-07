using TicketFly.Domain.Events;

namespace TicketFly.Domain.Entities;
public class Ticket : BaseEntity
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? Sender { get; set; }
    public string? Domain { get; set; }

    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && !_done)
            {
                AddDomainEvent(new TicketCompletedEvent(this));
            }

            _done = value;
        }
    }

    public TicketStatus Status { get; set; }
    public Guid? AssignedToId { get; set; }
    public Guid? ClientId { get; set; }

    public Client Client { get; set; } = null!;
    public User AssignedTo { get; set; } = null!;
    public IList<TicketMessage> Items { get; private set; } = [];
}
