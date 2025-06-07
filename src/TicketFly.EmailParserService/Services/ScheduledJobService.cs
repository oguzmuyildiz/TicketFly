using TicketFly.EmailParserService.Models;
using TicketFly.EmailParserService.Repositories;

namespace TicketFly.EmailParserService.Services
{
    public class ScheduledJobService
    {
        private readonly IScheduledJobRepository _scheduledJobRepository;

        public ScheduledJobService(IScheduledJobRepository scheduledJobRepository)
        {
            _scheduledJobRepository = scheduledJobRepository;
        }
        public async Task Add(ScheduledJob job)
        {
            await _scheduledJobRepository.AddAsync(job);
        }

        public async Task<IEnumerable<ScheduledJob>> GetAll()
        {
            return await _scheduledJobRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ScheduledJob>> GetNew()
        {
            return await _scheduledJobRepository.GetNewAsync();
        }
        
        public async Task<ScheduledJob> GetById(Guid jobId)
        {
            ScheduledJob? sJob = await _scheduledJobRepository.GetByIdAsync(jobId);
            return sJob ?? throw new KeyNotFoundException($"Scheduled job with ID {jobId} not found.");
        }

        public async Task Update(ScheduledJob job)
        {
            await _scheduledJobRepository.UpdateAsync(job);
        }

        public async Task Remove(Guid jobId)
        {
            await _scheduledJobRepository.DeleteAsync(jobId);
        }
    }
}
