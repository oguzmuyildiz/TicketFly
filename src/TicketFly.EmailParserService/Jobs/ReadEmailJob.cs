using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.AspNetCore.Hosting.Server;
using MimeKit;
using Quartz;

namespace TicketFly.EmailParserService.Jobs
{
    public class ReadEmailJob : IJob
    {
        private List<MimeMessage> unseenMessages = [];
        public Task Execute(IJobExecutionContext context)
        {
            StartJob(context.JobDetail.Key.Name);
            return Task.CompletedTask;
        }

        public async void StartJob(string jobId)
        {
            int meailCount = await FetchEmails(jobId);
            if (meailCount > 0) {
                StartParsing();
            }

        }
        public async Task<int> FetchEmails(string jobId)
        {
            Console.WriteLine($"Executing FetchEmails with jobId {jobId}");

            using var client = await CreateClientAndConnect();
            
            // The Inbox folder is always available on all IMAP servers...
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            var orderBy = new[] { OrderBy.ReverseArrival };
            var searchQuery = SearchQuery.NotSeen;

            var uidList = await inbox.SortAsync(searchQuery, orderBy);

            Console.WriteLine("Total messages: {0}", inbox.Count);
            Console.WriteLine("Recent messages: {0}", inbox.Recent);
            foreach (var uid in uidList)
            {
                var message = inbox.GetMessage(uid);
                unseenMessages.Add(message);

                inbox.Store(uid, new StoreFlagsRequest(StoreAction.Add, MessageFlags.Seen) { Silent = true });
                Console.WriteLine("Subject: {0}", message.Subject);
            }
            client.Disconnect(true);

            return uidList.Count;
        }
        public async void StartParsing()
        {
            foreach (var item in unseenMessages)
            {
                Console.WriteLine("Received Subject: {0}", item.Subject);
            }
        }


        public async Task<ImapClient> CreateClientAndConnect()
        {
            try
            {
                var client = new ImapClient();
            
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(data.Server, data.Port, data.SSL);

                await client.AuthenticateAsync(data.Username, data.Password);

                return client.IsConnected && client.IsAuthenticated ? client : null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
