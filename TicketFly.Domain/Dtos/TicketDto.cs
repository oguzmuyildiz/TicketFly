using TicketFly.Domain.Entities;

namespace TicketFly.Domain.Dtos;

public class TicketDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public IList<TicketMessageDto> Items { get; set; }
}
