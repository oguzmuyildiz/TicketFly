namespace TicketFly.Domain.Dtos;
public record TicketMessageDto(Guid Id, string? Title, string? Content, int Priority);