using Microsoft.EntityFrameworkCore;
using TicketFly.EmailParserService.Data;
using TicketFly.EmailParserService.Models;

namespace TicketFly.EmailParserService.Repositories
{
    public class ScheduledJobRepository(AppDbContext dbContext) : IScheduledJobRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task AddAsync(ScheduledJob job)
        {
            _dbContext.ScheduledJobs.Add(job);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingJob = _dbContext.ScheduledJobs.Find(id);
            if (existingJob != null)
            {
                _dbContext.ScheduledJobs.Remove(existingJob);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ScheduledJob>> GetAllAsync()
        {
            return await _dbContext.ScheduledJobs.ToListAsync();
        }

        public async Task<ScheduledJob?> GetByIdAsync(Guid id)
        {
            return await _dbContext.ScheduledJobs.FindAsync(id);
        }

        public async Task<IEnumerable<ScheduledJob>> GetNewAsync()
        {
            return await _dbContext.ScheduledJobs.Where(x=>x.Status == Enums.JobStatus.New).ToListAsync();
        }

        public async Task UpdateAsync(ScheduledJob job)
        {
            var existingJob = _dbContext.ScheduledJobs.Find(job.Id);
            if (existingJob != null)
            {
                _dbContext.Entry(existingJob).CurrentValues.SetValues(job);
               await _dbContext.SaveChangesAsync();
            }
        }
    }
}
