namespace TicketFly.Application.Organizations.Queries.Get;
public record GetOrganizationsQuery : IRequest<Result<IEnumerable<OrganizationDto>>>;