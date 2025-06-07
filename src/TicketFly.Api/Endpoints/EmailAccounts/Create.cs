using TicketFly.Application.EmailAccounts.Commands.Create;
using TicketFly.Domain.Constants;
using TicketFly.Domain.Enums;

namespace TicketFly.Api.Endpoints.EmailAccounts;

public record CreateEmailAccountRequest(Guid OrganizationId, 
    string Host, string Email, string ApiKey, 
    MailProviders Provider, int Port, bool IsActive);

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("emailaccounts", CreateEmailAccount)
            .WithTags(EndpointTags.EmailAccounts)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> CreateEmailAccount(CreateEmailAccountRequest request, 
        ISender sender, CancellationToken cancellationToken)
    {
        Result<Guid> result = await sender.Send(
            new CreateEmailAccountCommand(request.OrganizationId, request.Host, request.Email, 
            request.ApiKey, request.Provider, request.Port, request.IsActive), cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
