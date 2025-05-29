using Microsoft.EntityFrameworkCore;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Common.Intefaces.Data;

public interface IAppDbContext
{
    DbSet<Client> Clients { get; }

    DbSet<Ticket> Tickets { get; }
    DbSet<User> Users { get; }
    DbSet<TicketMessage> TicketMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
