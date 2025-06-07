using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using System.Reflection;

using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Api.Services;
using Serilog;
using Serilog.Sinks.OpenTelemetry;
using RabbitMQ.Shared;
using MassTransit;
using MassTransit.Configuration;
using RabbitMQ.Shared.Models.EmailParser.Events;
using Microsoft.Extensions.Options;
using TicketFly.Api.Consumers;

namespace TicketFly.Api;

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

    public static void AddSerilogServices(this IHostApplicationBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
       .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.OpenTelemetry(x =>
            {
                x.Endpoint = builder.Configuration["OtlpExporter:IngestUrl"];
                x.Protocol = OtlpProtocol.HttpProtobuf;
                x.Headers = new Dictionary<string, string>
                {
                    { "X-Seq-ApiKey", builder.Configuration["OtlpExporter:ApiKey"] }
                };
                x.ResourceAttributes = new Dictionary<string, object>
                {
                    ["service.name"] = "TicketFly.WebApi",
                    ["service.version"] = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown",
                    ["environment"] = builder.Environment.EnvironmentName
                };  
            })
            .CreateLogger();
        builder.Services.AddSerilog();
    }

    public static void AddMassTransitServices(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));
        builder.Services.AddMassTransit(x =>
        {
            x.RegisterConsumer<NewEmailReceivedEventConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMQSettings = context.GetRequiredService<IOptions<RabbitMQSettings>>().Value;
                cfg.Host(rabbitMQSettings.Host, rabbitMQSettings.VirtualHost, host =>
                {
                    host.Username(rabbitMQSettings.Username);
                    host.Password(rabbitMQSettings.Password);
                });
                cfg.RegisterQueue<NewEmailReceivedEventConsumer>(context, rabbitMQSettings.QueueName, typeof(NewEmailReceivedEvent));
            });
        });
    }
    public static void UseWebServices(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
        });

        app.UseExceptionHandler();
        app.MapEndpoints();
    }

}
