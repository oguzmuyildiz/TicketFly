namespace TicketFly.Domain.Entities;
public class Client : BaseEntity
{
    public required Guid OrganizationId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Domain { get; set; }
    public Organization? Organization { get; set; }
    public IList<Ticket> Tickets { get; private set; } = [];
}
