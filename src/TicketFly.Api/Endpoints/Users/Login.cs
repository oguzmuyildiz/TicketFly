using TicketFly.Application.Users.Commands.Login;
using TicketFly.Domain.Models;

namespace TicketFly.Api.Endpoints.Users;

public record LoginUserRequest(string Email, string Password);

public class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("users/login", LoginUser)
            .WithTags(EndpointTags.Clients);
    }

    public static async Task<IResult> LoginUser(LoginUserRequest request, ISender sender, CancellationToken cancellationToken)
    {
        LoginUserCommand command = new(request.Email, request.Password);
        Result<TokenModel> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
