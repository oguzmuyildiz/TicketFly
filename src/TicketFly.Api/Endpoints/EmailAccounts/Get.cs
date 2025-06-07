using TicketFly.Application.EmailAccounts.Queries.Get;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.EmailAccounts;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("emailaccounts", GetEmailAccounts)
            .WithTags(EndpointTags.EmailAccounts)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetEmailAccounts(ISender sender, ILogger logger)
    {
        logger.LogInformation("Handling request to get all Email Accounts");

        Result<IEnumerable<EmailAccountDto>> result = await sender.Send(new GetEmailAccountsQuery());
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
