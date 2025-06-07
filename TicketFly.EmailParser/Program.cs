using MailKit.Net.Imap;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Shared;
using RabbitMQ.Shared.Models.TicketApi.Events;
using TicketFly.EmailParser.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IImapClient, ImapClient>();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));
builder.Services.AddMassTransit(x =>
{
    x.RegisterConsumer<EmailAccountCreatedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var massTransitSettings = context.GetRequiredService<IOptions<RabbitMQSettings>>().Value;
        cfg.Host(massTransitSettings.Host, massTransitSettings.VirtualHost, host =>
        {
            host.Username(massTransitSettings.Username);
            host.Password(massTransitSettings.Password);
        });

        cfg.RegisterQueue<EmailAccountCreatedEventConsumer>(context, massTransitSettings.QueueName, typeof(EmailAccountCreatedEvent));
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
