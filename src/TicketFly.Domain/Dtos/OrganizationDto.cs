namespace TicketFly.Domain.Dtos;

public record OrganizationDto(Guid Id, string Name, string Description, string Logo, string Website, string ContactEmail, string ContactPhone);