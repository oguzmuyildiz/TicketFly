using MailKit.Net.Imap;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using TicketFly.Shared;
using TicketFly.Shared.Models;
using TicketFly.Shared.Models.TicketApi.Events;
using TicketFly.EmailParser.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IImapClient, ImapClient>();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));
builder.Services.AddMassTransit(x =>
{
    x.RegisterConsumer<EmailAccountCreatedEventConsumer>();
    x.RegisterConsumer<TestCommandConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var massTransitSettings = context.GetRequiredService<IOptions<RabbitMQSettings>>().Value;
        cfg.Host(massTransitSettings.Host, massTransitSettings.VirtualHost, host =>
        {
            host.Username(massTransitSettings.Username);
            host.Password(massTransitSettings.Password);
        });

        cfg.RegisterQueue<EmailAccountCreatedEventConsumer>(context, massTransitSettings.QueueName, typeof(EmailAccountCreatedEvent));
        cfg.RegisterQueue<TestCommandConsumer>(context, massTransitSettings.QueueName, typeof(TestCommand));
    });
});

var app = builder.Build();

app.MapGet("/", () => "emailparser service running!");

app.Run();
