using TicketFly.Application.Organizations.Queries.GetById;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.Organizations;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("organizations/{Id:guid}", GetOrganizationById)
            .WithTags(EndpointTags.Organizations)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetOrganizationById(Guid Id, ISender sender, 
        CancellationToken cancellationToken)
    {
        Result<OrganizationDto> result = await sender.Send(new GetOrganizationByIdQuery(Id), 
            cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

