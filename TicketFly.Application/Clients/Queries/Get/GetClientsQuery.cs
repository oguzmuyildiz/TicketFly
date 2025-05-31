using TicketFly.Domain.Common;
using TicketFly.Domain.Dtos;

namespace TicketFly.Application.Clients.Queries.Get;
public record GetClientsQuery : IRequest<Result<IEnumerable<ClientDto>>>;