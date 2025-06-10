namespace TicketFly.Domain.Entities;
public class Organization : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Logo { get; set; }
    public string? Website { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
}
