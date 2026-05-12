using MaintenanceManager.Ai;
using MaintenanceManager.Api.Workers;
using MaintenanceManager.Core;
using MaintenanceManager.Infra;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfra();
builder.Services.AddAgents();

Configuration.OpenApiKey = builder.Configuration.GetValue<string>("OpenApiKey")
    ?? throw new InvalidOperationException("OpenApiKey is not configured");

builder.Services.AddHostedService<SendTasksWorker>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
