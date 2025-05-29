using TicketFly.Domain.Dtos;

namespace TicketFly.Application.Clients.Queries.Get;
public record GetClientGetByIdQuery : IRequest<IEnumerable<ClientDto>>;