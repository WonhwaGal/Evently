
namespace Evently.Modules.Events.PublicApi;
public interface IEventsApi
{
    Task<TicketTypeResponse?> GetAsync(Guid ticketTypeId, CancellationToken cancellationToken);
}
