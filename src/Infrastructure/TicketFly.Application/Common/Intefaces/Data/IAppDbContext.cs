namespace TicketFly.Application.Common.Intefaces.Data;
public interface IAppDbContext
{
    DbSet<Organization> Organizations { get; }
    DbSet<EmailAccount> EmailAccounts { get; }
    DbSet<Client> Clients { get; }
    
    DbSet<Ticket> Tickets { get; }
    DbSet<TicketAlarmRule> TicketAlarmRules { get; }
    DbSet<TicketMessage> TicketMessages { get; }

    DbSet<User> Users { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<Role> Roles { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<AppNotification> AppNotifications { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
