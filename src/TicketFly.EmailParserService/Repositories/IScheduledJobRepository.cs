using TicketFly.EmailParserService.Models;

namespace TicketFly.EmailParserService.Repositories;

public interface IScheduledJobRepository
{
    Task<ScheduledJob?> GetByIdAsync(Guid id);
    Task<IEnumerable<ScheduledJob>> GetAllAsync();
    Task<IEnumerable<ScheduledJob>> GetNewAsync();
    Task AddAsync(ScheduledJob job);
    Task UpdateAsync(ScheduledJob job);
    Task DeleteAsync(Guid id);
}
