using TicketFly.Domain.Dtos;

namespace TicketFly.Application.Clients.Queries.Get;
public record GetClientsQuery : IRequest<IEnumerable<ClientDto>>;