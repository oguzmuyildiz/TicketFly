using Microsoft.EntityFrameworkCore;
using Quartz;
using TicketFly.EmailParserService;
using TicketFly.EmailParserService.Data;
using TicketFly.EmailParserService.Repositories;
using TicketFly.EmailParserService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SchedulerDbConnection")));
builder.Services.AddScoped<IAppDbContext>(provider => (IAppDbContext)provider.GetRequiredService<AppDbContext>());

builder.Services.AddScoped<IScheduledJobRepository, ScheduledJobRepository>();
builder.Services.AddScoped<ScheduledJobService>();

builder.Services.AddHostedService<ExampleHostedService>();


// Add the required Quartz.NET services
builder.Services.AddQuartz();
// Add the Quartz.NET hosted service

builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
