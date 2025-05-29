using Microsoft.EntityFrameworkCore;
using TicketFly.Domain.Entities;

namespace TicketFly.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketMessage> TicketMessages { get; set; }
}
