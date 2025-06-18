using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.Endpoints;
using Evently.Modules.Attendance.Application.Attendees.CheckInAttendee;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Attendance.Presentation.Attendees;

internal sealed class CheckInAttendee : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("attendees/check-in", async (
                Request request,
                ISender sender) =>
            {
                Result result = await sender.Send(
                    new CheckInAttendeeCommand(
                        request.CustomerId,
                        request.TicketId));

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .WithTags(Tags.Attendees);
    }

    internal sealed class Request
    {
        /// <summary>
        /// Идентификатор покупателя
        /// </summary>
        public Guid CustomerId { get; init; }
        
        /// <summary>
        /// Идентификатор билета
        /// </summary>
        public Guid TicketId { get; init; }
    }
}
