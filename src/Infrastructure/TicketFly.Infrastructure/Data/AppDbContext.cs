using Microsoft.EntityFrameworkCore;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;

namespace TicketFly.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<EmailAccount> EmailAccounts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketMessage> TicketMessages { get; set; }
    public DbSet<TicketAlarmRule> TicketAlarmRules { get; set; }
    public DbSet<AppNotification> AppNotifications { get; set; }
}
