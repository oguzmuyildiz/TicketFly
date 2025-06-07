using TicketFly.Application.EmailAccounts.Queries.GetById;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.EmailAccounts;
public class GetById : IEndpoint
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
        Result<EmailAccountDto> result = await sender.Send(new GetEmailAccountsByIdQuery(Id), 
            cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

