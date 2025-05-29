namespace TicketFly.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}
