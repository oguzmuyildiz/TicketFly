namespace TicketFly.Domain.Entities;

public class EmailAccount : BaseEntity
{
    public required Guid OrganizationId { get; set; }
    public required MailProviders Provider { get; set; }
    public required string Email { get; set; } = string.Empty;
    public required string ApiKey { get; set; } = string.Empty;
    public required string Host { get; set; } = string.Empty;
    public required int Port { get; set; }
    public bool IsActive { get; set; }

    public Organization Organization { get; set; } = null!;
}
