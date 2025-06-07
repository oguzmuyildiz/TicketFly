namespace TicketFly.Domain.Dtos;

public record EmailAccountDto(Guid Id, Guid OrganizationId, string Host, string Email, string ApiKey, MailProviders Provider, int Port, bool IsActive)
{
    public Guid Id { get; set; } = Id;
    public Guid OrganizationId { get; set; } = OrganizationId;
    public string Host { get; set; } = Host;
    public string Email { get; set; } = Email;
    public string ApiKey { get; set; } = ApiKey;
    public MailProviders Provider { get; set; } = Provider;
    public int Port { get; set; } = Port;
    public bool IsActive { get; set; } = IsActive;
}
