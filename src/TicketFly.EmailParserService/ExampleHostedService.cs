using Quartz;
using TicketFly.EmailParserService.Jobs;
using TicketFly.EmailParserService.Services;

namespace TicketFly.EmailParserService;
public class ExampleHostedService : IHostedService, IDisposable
{
    private readonly ILogger<ExampleHostedService> _logger;
    private readonly ScheduledJobService _scheduledJobService;
    private readonly ISchedulerFactory _schedulerFactory;
    private Timer? _timer = null;

    public ExampleHostedService(ILogger<ExampleHostedService> logger, ScheduledJobService scheduledJobService, ISchedulerFactory schedulerFactory)
    {
        _logger = logger;
        _schedulerFactory = schedulerFactory;
        _scheduledJobService = scheduledJobService;
    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        var NewJobs = await _scheduledJobService.GetNew();
        var _scheduler = await _schedulerFactory.GetScheduler(stoppingToken);

        foreach (var job in NewJobs)
        {
            var jobDetail = JobBuilder.Create<ReadEmailJob>()
                .WithIdentity(job.Id.ToString(), "readmail")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(job.Id.ToString(), job.OrganizationId.ToString())
                .WithCronSchedule(job.Expression)
                .Build();

            await _scheduler.ScheduleJob(jobDetail, trigger, stoppingToken);
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}