namespace Evently.Modules.Ticketing.PublicApi;

public interface ITicketingApi
{
    Task CreateCustomerAsync(
        Guid customerId,
        string email,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default);

    Task CreateEventAsync(
        Guid id,
        Guid categotyId,
        string title,
        string description,
        string location,
        DateTime startAtUtc,
        DateTime? endAtUtc,
        CancellationToken cancellationToken = default);

    Task CreateTicketTypeAsync(
        Guid ticketTypeId,
        Guid eventId,
        string name,
        decimal price,
        string currency,
        decimal quantity,
        CancellationToken cancellationToken = default);
}
