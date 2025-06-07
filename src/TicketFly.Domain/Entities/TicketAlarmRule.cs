namespace TicketFly.Domain.Entities;

public class TicketAlarmRule : BaseAuditableEntity
{
    public required Guid OrganizationId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Duration { get; set; }
    public TimePeriods Period { get; set; }
    public Organization Organization { get; set; } = null!;
}
