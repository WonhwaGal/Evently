using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Module.Events.Api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Evently.Module.Events.Api.Events;

/// <summary>
/// Вертикальный слой мероприятий: получение информации о мероприятии
/// </summary>
public static class GetEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id}", async (Guid id, EventsDbContext context) =>
        {
            EventResponse? @event = await context.Events
                    .Where(e => e.Id == id)
                    .Select(e => new EventResponse(
                        e.Id,
                        e.Name,
                        e.Description,
                        e.Location,
                        e.StartAtUtc,
                        e.EndAtUtc,
                        e.Status))
                    .SingleOrDefaultAsync();

            return @event == null ? Results.NotFound() : Results.Ok(@event.Id);
        });

    }
    
    internal sealed class EventResponse
    {
        public EventResponse(
            Guid id, 
            string name, 
            string description, 
            string location, 
            DateTime startAtUtc,
            DateTime endAtUtc,
            EventStatus status)
        {
            Id = id; 
            Name = name; 
            Description = description; 
            Location = location; 
            StartAtUtc = startAtUtc; 
            EndAtUtc = endAtUtc; 
            Status = status;
        }

        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Место проведения
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        ///  Дата и время мероприятия
        /// </summary>
        public DateTime StartAtUtc { get; set; }

        /// <summary>
        /// Дата и время окончания мероприятия
        /// </summary>
        public DateTime EndAtUtc { get; set; }

        /// <summary>
        /// Статус мероприятия
        /// </summary>
        public EventStatus Status { get; set; }
    }
}
