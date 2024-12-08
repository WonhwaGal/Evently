using Evently.Api.Extensions;
using Evently.Modules.Events.Infrastructure;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using System.Reflection.Metadata;
using Evently.Modules.Users.Infrastructure;
using Serilog;
using Evently.Api.Middleware;
using Evently.Modules.Ticketing.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region [!] Решение сквозной проблемы: логирование (Serilog + Seq)

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

#endregion

#region [!] Решение сквозной проблемы: обработка исключений

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

#endregion

#region [!] Решение сквозной проблемы: внедрение зависимостей

builder.Services.AddApplication(
    [Evently.Modules.Events.Application.AssemblyReference.Assembly,
     Evently.Modules.Users.Application.AssemblyReference.Assembly,
     Evently.Modules.Ticketing.Application.AssemblyReference.Assembly]);

builder.Services.AddInfrastructure(builder.Configuration);

#endregion

#region [!] Решение сквозной проблемы: конфигурирование

builder.Configuration.AddModuleConfiguration(["events", "users", "ticketing"]);

#endregion

// Modules
builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

//Register module endpoints 
EventsModule.MapEndpoints(app);
UsersModule.MapEndpoints(app);
TicketingModule.MapEndpoints(app);

#region [!] Решение сквозной проблемы: обработка исключений

app.UseExceptionHandler();

#endregion

#region [!] Решение сквозной проблемы: логирование (Serilog + Seq)

app.UseSerilogRequestLogging();

#endregion

await app.RunAsync();
