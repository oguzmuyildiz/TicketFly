using Microsoft.EntityFrameworkCore;
using TicketFly.EmailParserService.Models;

namespace TicketFly.EmailParserService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ScheduledJob> ScheduledJobs { get; set; }
       
    }
}
