using MassTransit;
using Serilog;
using TicketFly.Shared.Models;
using TicketFly.Api;
using TicketFly.Application;
using TicketFly.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var pp = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD", EnvironmentVariableTarget.Machine);

//builder.AddSerilogServices();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Logger.Warning("Using Serilog for logging. MSSQL_SA_PASSWORD: {Password}", pp);

builder.Services.AddSerilog();

builder.Host.UseSerilog();

builder.AddMassTransitServices();

builder.Services.AddOpenApi();

builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

var app = builder.Build();

//await app.InitialiseDatabaseAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//app.UseHttpsRedirection();

app.UseWebServices();

app.MapGet("/", () => {
    return "Say hi to web api!";
});

app.MapGet("/test", async (IBus bus) => {
    await bus.Publish(new TestCommand("TestCommand works!"));
    return "Say hi to web api!";
});

app.Run();
