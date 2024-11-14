using Evently.Api.Extensions;
using Evently.Modules.Events.Infrastructure;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using System.Reflection.Metadata;
using Evently.Modules.Users.Infrastructure;
using Serilog;
using Evently.Api.Middleware;

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
     Evently.Modules.Users.Application.AssemblyReference.Assembly]);

builder.Services.AddInfrastructure(builder.Configuration);

#endregion

#region [!] Решение сквозной проблемы: конфигурирование

builder.Configuration.AddModuleConfiguration(["events", "users"]);

#endregion

// Modules
builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);

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

#region [!] Решение сквозной проблемы: обработка исключений

app.UseExceptionHandler();

#endregion

#region [!] Решение сквозной проблемы: логирование (Serilog + Seq)

app.UseSerilogRequestLogging();

#endregion

await app.RunAsync();
