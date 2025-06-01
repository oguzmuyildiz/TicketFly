
using TicketFly.Domain.Entities;
using TicketFly.Domain.Models;

namespace TicketFly.Application.Common.Intefaces.Authentication;

public interface ITokenProvider
{
    TokenModel Create(User user, string IpAddress);
}
