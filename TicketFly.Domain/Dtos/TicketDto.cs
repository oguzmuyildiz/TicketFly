namespace TicketFly.Domain.Dtos;
public class TicketDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Sender { get; set; }
    public string? Domain { get; set; }
    public int Status { get; set; }
    public string? AssignedToId { get; set; }
    public string? ClientId { get; set; }
    public IList<TicketMessageDto> Items { get; set; }
}
