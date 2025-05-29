using TicketFly.Domain.Dtos;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Common.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDto>();
    }
}