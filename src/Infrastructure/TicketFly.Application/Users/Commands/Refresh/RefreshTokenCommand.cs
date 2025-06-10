using TicketFly.Domain.Models;

namespace TicketFly.Application.Users.Commands.Refresh;
public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenModel>>;
