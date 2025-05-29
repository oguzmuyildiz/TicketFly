using System.Reflection;
using TicketFly.Application;
using TicketFly.Infrastructure;
using TicketFly.WebApi;
using TicketFly.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.AddWebServices();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Say hi to web api!");

app.Run();
