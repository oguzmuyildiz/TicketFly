namespace TicketFly.Domain.Dtos;
public record TicketDto(Guid Id,
    string? Title,
    string? Content,
    string? Sender,
    string? Domain,
    int Status,
    string? AssignedToId,
    string? ClientId,
    IList<TicketMessageDto> Items)
{
    public Guid Id { get; set; } = Id;
    public string? Title { get; set; } = Title;
    public string? Content { get; set; } = Content;
    public string? Sender { get; set; } = Sender;
    public string? Domain { get; set; } = Domain;
    public int Status { get; set; } = Status;
    public string? AssignedToId { get; set; } = AssignedToId;
    public string? ClientId { get; set; } = ClientId;
    public IList<TicketMessageDto> Items { get; set; } = Items ?? [];
}
