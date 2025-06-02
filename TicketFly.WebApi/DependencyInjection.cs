using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using System.Reflection;
using OpenTelemetry.Logs;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;

using TicketFly.Application.Common.Intefaces.Authentication;
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

    public static void AddOpenTelemetryServices(this IHostApplicationBuilder builder)
    {
        var OtlpExporterHeader = builder.Configuration["OtlpExporter:Headers"];
        var OtlpExporterIngestUrl = builder.Configuration["OtlpExporter:IngestUrl"];

        builder.Logging.ClearProviders();
        builder.Services.AddLogging(logging => logging.AddOpenTelemetry(openTelemetryLoggerOptions =>
        {
            openTelemetryLoggerOptions.SetResourceBuilder(
                ResourceBuilder.CreateEmpty()
                    .AddService("TicketFly.WebApi")
                    .AddAttributes(new Dictionary<string, object>
                    {
                        ["Environment"] = builder.Environment.EnvironmentName
                    }));

            openTelemetryLoggerOptions.IncludeScopes = true;
            openTelemetryLoggerOptions.IncludeFormattedMessage = true;

            openTelemetryLoggerOptions.AddOtlpExporter(exporter =>
            {
                exporter.Endpoint = new Uri(OtlpExporterIngestUrl);
                exporter.Protocol = OtlpExportProtocol.HttpProtobuf;
                exporter.Headers = OtlpExporterHeader;
            });
        }));
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
