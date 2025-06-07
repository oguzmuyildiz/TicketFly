namespace TicketFly.Domain.Dtos;
public record UserDto(Guid Id, string? FirstName, string? LastName, string Email);