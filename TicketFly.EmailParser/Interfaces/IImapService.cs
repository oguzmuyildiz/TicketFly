using TicketFly.EmailParser.Models;

namespace TicketFly.EmailParser.Interfaces
{
    public interface IImapService
    {
        public Task CreateClientAndConnect(ScheduledJobModel data);
        public Task DisconnectAsync();
        public Task<List<IncomingMail>> GetSortedNewEmails();
    }
}
