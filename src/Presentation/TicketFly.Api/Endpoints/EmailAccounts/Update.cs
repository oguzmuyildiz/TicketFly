using TicketFly.Application.EmailAccounts.Commands.Update;
using TicketFly.Domain.Constants;
using TicketFly.Domain.Enums;

namespace TicketFly.Api.Endpoints.EmailAccounts;

public record UpdateEmailAccountRequest(
    Guid Id,
    Guid OrganizationId,
    string Host, 
    string Email, 
    string ApiKey,
    MailProviders Provider, 
    int Port, 
    bool IsActive);

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPut("emailaccount", UpdateEmailAccount)
            .WithTags(EndpointTags.EmailAccounts)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> UpdateEmailAccount(UpdateEmailAccountRequest request, 
        ISender sender, CancellationToken cancellationToken)
    {
        Result<bool> result = await sender.Send(
            new UpdateEmailAccountCommand(request.Id, request.OrganizationId, request.Host, 
                request.Email, request.ApiKey, request.Provider, request.Port, request.IsActive), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
