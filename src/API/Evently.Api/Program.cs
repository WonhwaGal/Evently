using Serilog;
using Evently.Api.Middleware;
using Evently.Api.Extensions;
using Evently.Modules.Events.Infrastructure;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Modules.Users.Infrastructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Evently.Api.OpenTelemetry;
using Evently.Common.Infrastructure.Configuration;
using Evently.Common.Infrastructure.EventBus;
using Evently.Common.Presentation.Endpoints;
using Evently.Modules.Attendance.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region [!] ������� �������� ��������: ����������� (Serilog + Seq)

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

#endregion

#region [!] ������� �������� ��������: ��������� ����������

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

#endregion

#region [!] ������� �������� ��������: ��������� ������������

builder.Services.AddApplication(
    [Evently.Modules.Events.Application.AssemblyReference.Assembly,
     Evently.Modules.Users.Application.AssemblyReference.Assembly,
     Evently.Modules.Attendance.Application.AssemblyReference.Assembly]);

var rabbitMqSettings = new RabbitMqSettings(builder.Configuration.GetConnectionStringOrThrow("Queue"));
builder.Services.AddInfrastructure(
    builder.Logging,
    DiagnosticsConfig.ServiceName,
    [
        EventsModule.ConfigureConsumers,
        UsersModule.ConfigureConsumers,
        AttendanceModule.ConfigureConsumers
    ],
    rabbitMqSettings,
    builder.Configuration);

#endregion

#region [!] ������� �������� ��������: ����������������

builder.Configuration.AddModuleConfiguration(["events", "users", "attendance"]);

#endregion

// Modules
builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddAttendanceModule(builder.Configuration);

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
    
    //Настройка генерации идентификаторов схем
    options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.MapEndpoints();

#region [!] ������� �������� ��������: ��������� ����������

app.UseExceptionHandler();

#endregion

#region [!] ������� �������� ��������: ����������� (Serilog + Seq)

app.UseSerilogRequestLogging();

#endregion

#region [!] ������� �������� ��������: �������������� � �����������

app.UseAuthentication();
app.UseAuthorization();

#endregion

await app.RunAsync();


public partial class Program;
