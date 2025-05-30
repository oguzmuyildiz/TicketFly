namespace TicketFly.Domain.Dtos;

public class TicketMessageDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }

    public string? Content { get; set; }

    public int Priority { get; set; }
}
