using System.Reflection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TicketFly.Application.Common.Behavior;

namespace TicketFly.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        builder.Services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }
}