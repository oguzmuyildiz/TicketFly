using TicketFly.Application.Organizations.Queries.Get;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.Organizations;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("organizations", GetOrganizations)
            .WithTags(EndpointTags.Organizations)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetOrganizations(ISender sender, ILogger<Get> logger)
    {
        logger.LogInformation("Handling request to get all Organizations");

        Result<IEnumerable<OrganizationDto>> result = await sender.Send(new GetOrganizationsQuery());
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
