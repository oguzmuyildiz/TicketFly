using Microsoft.AspNetCore.Mvc;
using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.WebApi.Services;

namespace TicketFly.WebApi;

public static class DependencyInjection
{
    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserContext, UserContext>();
        builder.Services.AddHttpContextAccessor();
    }
}
