using TicketFly.EmailParserService.Enums;

namespace TicketFly.EmailParserService.Models
{
    public class ScheduledJob
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public required MailProviders Provider { get; set; }
        public required string Expression { get; set; }
        public required string EmailAddress { get; set; }
        public required string Host { get; set; }
        public required string Port { get; set; }
        public required string ApiKey { get; set; }
        public bool IsActive { get; set; }
        public JobStatus Status { get; set; }
    }
}
