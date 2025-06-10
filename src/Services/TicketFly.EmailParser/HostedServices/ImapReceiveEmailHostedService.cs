using TicketFly.EmailParser.Services;

namespace TicketFly.EmailParser.HostedServices;

public class ImapReceiveEmailHostedService(ImapService imapService) : IHostedService
{
    private readonly ImapService imapService = imapService;

    public Task StartAsync(CancellationToken cancellationToken) => _ = Loop(cancellationToken);

    private async Task Loop(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            //ScheduledJobModel scheduledJobs = new ScheduledJobModel();
            //imapService.CreateClientAndConnect();
            //ImapService service = new ImapService();
            //ScheduledJobModel scheduledJobs = new ScheduledJobModel();
            // Here you would typically retrieve the scheduled jobs from a database or configuration
            //var tasks = CheckNewMails(service, scheduledJobs);

            //await Task.WhenAll(tasks);
            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
