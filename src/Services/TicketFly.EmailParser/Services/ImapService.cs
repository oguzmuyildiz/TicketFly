using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit.Text;
using TicketFly.EmailParser.Interfaces;
using TicketFly.EmailParser.Models;

namespace TicketFly.EmailParser.Services;

public class ImapService : IImapService
{
    ImapClient imapClient;

    public ImapService() : base()
    {
    }

    public async Task CreateClientAndConnect(ScheduledJobModel data)
    {
        try
        {
            imapClient = new ImapClient
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };
            await imapClient.ConnectAsync(data.Server, data.Port, data.Ssl);
            await imapClient.AuthenticateAsync(data.Username, data.ApiKey);

        }
        catch (Exception)
        {
            imapClient = null;
        }
    }


    public async Task<List<IncomingMail>> GetSortedNewEmails()
    {
        var inbox = imapClient.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadOnly);

        var orderBy = new[] { OrderBy.ReverseArrival };
        var searchQuery = SearchQuery.NotSeen;

        var emailIds = await inbox.SortAsync(searchQuery, orderBy);
        List<IncomingMail> IncomingMails = [];

        foreach (var emailId in emailIds)
        {
            var email = await inbox.GetMessageAsync(emailId);
            var TextBody = email.GetTextBody(TextFormat.Plain);
            var Subject = email.Subject;
            IncomingMails.Add(new IncomingMail { Body = TextBody, Subject = Subject, From = email.From.ToString(), UniqueId = emailId.ToString() });
            inbox.AddFlags(emailId, MessageFlags.Seen, true);
        }
        return IncomingMails;
    }

    public async Task DisconnectAsync()
    {
        if (imapClient.IsConnected)
        {
            await imapClient.DisconnectAsync(true);
        }
        imapClient.Dispose();
    }
}
