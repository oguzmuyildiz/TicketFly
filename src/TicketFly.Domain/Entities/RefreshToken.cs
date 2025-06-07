namespace TicketFly.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }

    public required string Token { get; set; }
    public required DateTimeOffset Expires { get; set; }
    public required DateTimeOffset Created { get; set; }
    public required string CreatedByIp { get; set; }
    public required bool IsActive { get; set; } = true;
    public bool IsExpired => DateTimeOffset.UtcNow >= Expires;

    // Navigation property
    public User User { get; set; } = null!;
}
