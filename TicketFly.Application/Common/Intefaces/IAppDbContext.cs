using Microsoft.EntityFrameworkCore;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Client> Clients { get; }

    DbSet<Ticket> Tickets { get; }
    DbSet<TicketMessage> TicketMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
