using TicketFly.Domain.Common;
using TicketFly.Domain.Enums;

namespace TicketFly.Domain.Entities;

public class Ticket : BaseEntity
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Sender { get; set; }
    public string? Domain { get; set; }

    public TicketStatus Status { get; set; }
    public Guid AssignedTo { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public IList<TicketMessage> Items { get; private set; } = [];
}
