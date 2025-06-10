namespace TicketFly.Application.Common.MappingProfiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<Ticket, TicketDto>().ReverseMap();
        CreateMap<TicketMessage, TicketMessageDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<EmailAccount, EmailAccountDto>().ReverseMap();
        CreateMap<Organization, OrganizationDto>().ReverseMap();
    }
}