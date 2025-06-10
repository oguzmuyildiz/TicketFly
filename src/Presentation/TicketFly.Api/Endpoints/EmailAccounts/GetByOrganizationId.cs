using TicketFly.Application.EmailAccounts.Queries.GetByOrganizationId;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.EmailAccounts;

public class GetByOrganizationId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("emailaccounts/{Id:guid}", GetEmailAccountById)
            .WithTags(EndpointTags.EmailAccounts)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetEmailAccountById(Guid Id, ISender sender, 
        CancellationToken cancellationToken)
    {
        Result<IEnumerable<EmailAccountDto>> result = await sender.Send(new GetEmailAccountsByOrganizationIdQuery(Id), 
            cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

