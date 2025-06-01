using TicketFly.Domain.Entities;

namespace TicketFly.Application.Common.Intefaces.Data;

public interface IAppDbContext
{
    DbSet<Client> Clients { get; }
    DbSet<Ticket> Tickets { get; }
    DbSet<User> Users { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<Role> Roles { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<TicketMessage> TicketMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
