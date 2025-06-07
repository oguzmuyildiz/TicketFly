using TicketFly.Application.Users.Commands.Register;
using TicketFly.Domain.Models;

namespace TicketFly.Api.Endpoints.Users;

public record RegisterUserRequest(string Email, string UserName, string FirstName, string LastName, string Password);

public class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("users/register", RegisterUser)
            .WithTags(EndpointTags.Clients);
    }

    public static async Task<IResult> RegisterUser(RegisterUserRequest request, ISender sender, CancellationToken cancellationToken)
    {
        RegisterUserCommand command = new(request.Email, request.UserName, request.FirstName, request.LastName, request.Password);
        Result<TokenModel> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
