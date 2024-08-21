using Evently.Api.Extensions;
using Evently.Module.Events.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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
