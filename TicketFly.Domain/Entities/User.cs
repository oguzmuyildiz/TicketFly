namespace TicketFly.Domain.Entities;
public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public IList<Ticket> Tickets { get; private set; } = [];
    public IList<UserRole> UserRoles { get; private set; } = [];
}
