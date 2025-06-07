namespace TicketFly.EmailParser.Models
{
    public class IncomingMail
    {
        public required string UniqueId { get; set; }
        public required string From { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
