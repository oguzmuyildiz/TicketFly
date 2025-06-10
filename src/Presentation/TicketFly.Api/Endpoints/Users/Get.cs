using TicketFly.Application.Users.Queries.Get;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.Users;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("users", GetUsers)
            .WithTags(EndpointTags.Users)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetUsers(ISender sender)
    {
        Result<IEnumerable<UserDto>> result = await sender.Send(new GetUsersQuery());

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
