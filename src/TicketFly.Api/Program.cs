using TicketFly.Application;
using TicketFly.Infrastructure;
using TicketFly.Api;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogServices();
builder.AddMassTransitServices();

builder.Services.AddOpenApi();

builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.UseWebServices();

app.MapGet("/", () => "Say hi to web api!");
app.Run();
