using Evently.Ticketing.Api.Middleware;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using Evently.Common.Application;
using Evently.Common.Infrastructure.Configuration;
using Evently.Common.Infrastructure.EventBus;
using Evently.Common.Infrastructure;
using Evently.Ticketing.Api.OpenTelemetry;
using Evently.Modules.Ticketing.Infrastructure;
using Evently.Ticketing.Api.Extensions;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region [!] Решение сквозной проблемы: логирование (Serilog + Seq)
builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));
#endregion

#region [!] Решение сквозной проблемы: обработка исключений
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

#region [!] Решение сквозной проблемы: внедрение зависимостей
Assembly[] moduleApplicationAssemblies = [Evently.Modules.Ticketing.Application.AssemblyReference.Assembly];
builder.Services.AddApplication(moduleApplicationAssemblies);

var databaseSettings = new RabbitMqSettings(builder.Configuration.GetConnectionStringOrThrow("Database"));
var reddisSettings = new RabbitMqSettings(builder.Configuration.GetConnectionStringOrThrow("Cache"));
var rabbitMqSettings = new RabbitMqSettings(builder.Configuration.GetConnectionStringOrThrow("Queue"));
builder.Services.AddInfrastructure(
    builder.Logging,
    DiagnosticsConfig.ServiceName,
    [
        TicketingModule.ConfigureConsumers
    ],
    rabbitMqSettings,
    builder.Configuration);
#endregion

#region [!] Решение сквозной проблемы: конфигурирование
builder.Configuration.AddModuleConfiguration(["ticketing"]);
#endregion

builder.Services.AddTicketingModule(builder.Configuration);

builder.Services.AddAuthorization();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

TicketingModule.MapEndpoints(app);

#region [!] Решение сквозной проблемы: обработка исключений
app.UseExceptionHandler();
#endregion

#region [!] Решение сквозной проблемы: логирование (Serilog + Seq)
app.UseSerilogRequestLogging();
#endregion

#region [!] Решение сквозной проблемы: аутентификация и авторизация
app.UseAuthentication();
app.UseAuthorization();
#endregion

app.UseAuthorization();

await app.RunAsync();
