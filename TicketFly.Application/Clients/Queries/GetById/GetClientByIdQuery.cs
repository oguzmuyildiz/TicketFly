using TicketFly.Domain.Common;
using TicketFly.Domain.Dtos;

namespace TicketFly.Application.Clients.Queries.GetById;
public record GetClientByIdQuery(Guid Id) : IRequest<Result<ClientDto>>;