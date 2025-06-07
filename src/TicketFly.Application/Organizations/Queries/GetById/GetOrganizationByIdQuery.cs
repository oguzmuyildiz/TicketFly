namespace TicketFly.Application.Organizations.Queries.GetById;
public record GetOrganizationByIdQuery(Guid Id) : IRequest<Result<OrganizationDto>>;