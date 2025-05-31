using TicketFly.Domain.Common;

namespace TicketFly.Domain.Entities;

public class Role:BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
