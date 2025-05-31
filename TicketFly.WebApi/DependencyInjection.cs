using System.Reflection;
using Microsoft.AspNetCore.Http.Features;
using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.WebApi.Extensions;
using TicketFly.WebApi.Services;

namespace TicketFly.WebApi;

public static class DependencyInjection
{
    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserContext, UserContext>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

        builder.Services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance = $""
                    + $"{context.HttpContext.Request.Path}"
                    + $"{context.HttpContext.Request.Method}"
                    + $"{context.HttpContext.Request.QueryString}";
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;

                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
            };
        });
        //builder.Services.AddExceptionHandler<ProblemExceptionHandler>();
    }

    public static void UseWebServices(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.MapEndpoints();
    }
}
