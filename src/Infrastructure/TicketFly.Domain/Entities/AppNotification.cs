using TicketFly.Domain.Dtos;

namespace TicketFly.Domain.Entities;
public class AppNotification: BaseEntity
{
    public Guid? UserId { get; set; }
    public Guid? OrganizationId { get; set; }
    public Guid? RoleId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public bool IsRead { get; set; } = false;
    public string? Link { get; set; }
    public User? User { get; set; }
    public Organization? Organization { get; set; }
    public Role? Role { get; set; }
}
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppNotification, AppNotificationDto>().ReverseMap();
    }
}