using TicketFly.Application.Roles.Queries.Get;

namespace Api.HostedServices
{
    public class ExpiredTicketsService(ILogger<ExpiredTicketsService> logger, ISender sender) : BackgroundService
    {
        private readonly ILogger<ExpiredTicketsService> _logger = logger;
        private readonly ISender _sender = sender;

        // TODO: Consider Ticket Expiration Logic

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            // When the timer should have no due-time, then do the work once now.
            await CheckEmails();

            using PeriodicTimer timer = new(TimeSpan.FromMinutes(1));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await CheckEmails();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Timed Hosted Service is stopping.");
            }
        }

        private async Task CheckEmails()
        {
            int Count = 0;
            //Result<IEnumerable<TicketDto>> result = await _sender.Send(new GetExpiredTicketsQuery());
            //if(result.IsSuccess)
            //{
            //    Count = result.Value.Count();
            //    result.Value.ToList().ForEach(ticket =>
            //    {
            //        _logger.LogInformation("Expired Ticket: {TicketId}", ticket.Id);
            //    });
            //}
            // Simulate work
            await Task.Delay(TimeSpan.FromSeconds(2));

            _logger.LogInformation("Timed Hosted Service is working. Expired Ticket Count: {Count}", Count);
        }
    }
}

