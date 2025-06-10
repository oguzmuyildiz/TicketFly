namespace TicketFly.Domain.Entities;
public class TicketMessage : BaseAuditableEntity
{
    public Guid TicketId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public PriorityLevel Priority { get; set; }


    public Ticket Ticket { get; set; } = null!;
}
