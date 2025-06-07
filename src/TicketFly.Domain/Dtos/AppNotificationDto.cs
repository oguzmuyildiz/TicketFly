namespace TicketFly.Domain.Dtos;
public record AppNotificationDto(Guid Id, Guid? UserId, Guid? OrganizationId, Guid? RoleId, string Title, string Content, string? Link);