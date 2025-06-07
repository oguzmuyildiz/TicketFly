using Microsoft.EntityFrameworkCore;
using TicketFly.EmailParserService.Models;

namespace TicketFly.EmailParserService.Data;

public interface IAppDbContext
{
    DbSet<ScheduledJob> ScheduledJobs { get; set; }
}
