using TicketFly.Application;
using TicketFly.Infrastructure;
using TicketFly.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.AddWebServices();

var app = builder.Build();

app.UseWebServices();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.MapGet("/", () => "Say hi to web api!");

app.Run();
