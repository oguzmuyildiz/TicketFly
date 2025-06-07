using TicketFly.Application.Users.Commands.Refresh;
using TicketFly.Domain.Models;

namespace TicketFly.Api.Endpoints.Users;

public record RefreshRequest(string RefreshToken);
public class Refresh : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("users/refresh-token", RefreshUserToken)
            .WithTags(EndpointTags.Clients);
    }

    public static async Task<IResult> RefreshUserToken(RefreshRequest request, ISender sender, CancellationToken cancellationToken)
    {
        RefreshTokenCommand command = new(request.RefreshToken);
        Result<TokenModel> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
