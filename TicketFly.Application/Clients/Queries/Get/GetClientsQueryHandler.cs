using TicketFly.Domain.Dtos;

namespace TicketFly.Application.Clients.Queries.Get;

public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IEnumerable<ClientDto>>
{
    public async Task<IEnumerable<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        // Here you would typically interact with your database context to retrieve clients.
        return [];
    }
}
