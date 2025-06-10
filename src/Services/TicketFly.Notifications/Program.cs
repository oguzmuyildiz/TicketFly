using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using TicketFly.Shared;
using TicketFly.Shared.Models;
using TicketFly.Shared.Models.Notification.Command;
using TicketFly.Notifications.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));
builder.Services.AddMassTransit(x =>
{
    x.RegisterConsumer<SendEmailCommandConsumer>();
    x.RegisterConsumer<SendSmsCommandConsumer>();
    x.RegisterConsumer<TestCommandConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var massTransitSettings = context.GetRequiredService<IOptions<RabbitMQSettings>>().Value;
        cfg.Host(massTransitSettings.Host, massTransitSettings.VirtualHost, host =>
        {
            host.Username(massTransitSettings.Username);
            host.Password(massTransitSettings.Password);
        });

        cfg.RegisterQueue<SendEmailCommandConsumer>(context, massTransitSettings.QueueName, typeof(SendEmailCommand));
        cfg.RegisterQueue<SendSmsCommandConsumer>(context, massTransitSettings.QueueName, typeof(SendSmsCommand));
        cfg.RegisterQueue<TestCommandConsumer>(context, massTransitSettings.QueueName, typeof(TestCommand));
    });
});


var app = builder.Build();

app.MapGet("/", () => "notification service running!");


app.Run();
