namespace TicketFly.Domain.Entities;
public class Client : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Domain { get; set; }
    public IList<Ticket> Tickets { get; private set; } = [];
}
