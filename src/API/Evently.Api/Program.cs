using Evently.Api.Extensions;
using Evently.Modules.Events.Infrastructure;
using Evently.Common.Application;
using Evently.Common.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region [!] Решение сквозной проблемы: внедрение зависимостей

builder.Services.AddApplication(
    [Evently.Modules.Events.Application.AssemblyReference.Assembly]);

builder.Services.AddInfrastructure(builder.Configuration);

#endregion

#region [!] Решение сквозной проблемы: конфигурирование

builder.Configuration.AddModuleConfiguration(["events"]);

#endregion

// Events Module
builder.Services.AddEventsModule(builder.Configuration);

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

//Register endpoints 
EventsModule.MapEndpoints(app);

await app.RunAsync();
