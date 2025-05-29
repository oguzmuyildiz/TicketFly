
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Common.Intefaces.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
