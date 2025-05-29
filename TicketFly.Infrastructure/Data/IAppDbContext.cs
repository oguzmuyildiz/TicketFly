using Microsoft.EntityFrameworkCore;
using TicketFly.Domain.Entities;

namespace TicketFly.Infrastructure.Data;

public interface IAppDbContext
{
    DbSet<Client> Clients { get; }

    DbSet<Ticket> Tickets { get; }
    DbSet<TicketMessage> TicketMessages { get; }
}
