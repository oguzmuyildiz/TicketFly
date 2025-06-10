namespace TicketFly.EmailParser.Models
{
    public class ScheduledJobModel
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public required string Expression { get; set; }
        public required string Username { get; set; }
        public required string Server { get; set; }
        public required int Port { get; set; }
        public required bool Ssl { get; set; }
        public required string ApiKey { get; set; }
        public bool IsActive { get; set; }
    }
}
