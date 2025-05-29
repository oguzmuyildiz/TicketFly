using TicketFly.Domain.Dtos;

namespace TicketFly.Application.Clients.Queries.Get;

public class GetClientByIdQueryHandler : IRequestHandler<GetClientGetByIdQuery, IEnumerable<ClientDto>>
{
    public async Task<IEnumerable<ClientDto>> Handle(GetClientGetByIdQuery request, CancellationToken cancellationToken)
    {
        // Here you would typically interact with your database context to retrieve clients.
        return [];
    }
}
