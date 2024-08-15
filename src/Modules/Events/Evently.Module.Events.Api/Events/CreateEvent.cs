using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Module.Events.Api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Evently.Module.Events.Api.Events;

/// <summary>
/// Вертикальный слой мероприятий: создание мероприятия
/// Вариант использования: создание мероприятия
/// </summary>
public static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (CreateEventRequest request, EventsDbContext context) =>
        {
            var @event = new Event
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                StartAtUtc = request.StartAtUtc,
                EndAtUtc = request.EndAtUtc,
                Status = EventStatus.Draft
            };

            context.Events.Add(@event);

            await context.SaveChangesAsync();
        });
    }

    internal sealed class CreateEventRequest
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
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
    }
