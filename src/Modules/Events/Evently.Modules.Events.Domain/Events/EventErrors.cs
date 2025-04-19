using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Events;

public static class EventErrors
{
    public static readonly Error EndDatePrecedesStartDate = Error.Problem(
        "Events.EndDatePrecedesStartDate",
        "The event end date precedes the start date");

    public static Error NotFound(Guid eventId) =>
        Error.NotFound("Events.NoFound", $"The event with the identifier {eventId} was not found");

    public static readonly Error StartDateInPast = Error.Problem(
        "Events.StartDateInPast",
        $"The event start date is in the past");

    public static readonly Error EndDateInPast = Error.Problem(
        "Events.EndDateInPast",
        $"The event end date is in the past");

    public static readonly Error NotDraft = Error.Problem(
        "Events.NotDraft", "The event is not in draft status");
}
