using Evently.Modules.Ticketing.Application.Customers.CreateCustomer;
using Evently.Modules.Ticketing.Application.Events.CreateEvent;
using Evently.Modules.Ticketing.Application.TicketTypes.CreateTicketType;
using Evently.Modules.Ticketing.PublicApi;
using MediatR;

namespace Evently.Modules.Ticketing.Infrastructure.PublicApi;
internal sealed class TicketingApi(ISender sender) : ITicketingApi
{
    public async Task CreateCustomerAsync(
        Guid customerId,
        string email,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new CreateCustomerCommand(
                customerId, email, firstName, lastName), cancellationToken);
    }

    public async Task CreateEventAsync(Guid id, 
        Guid categotyId, 
        string title, 
        string description, 
        string location, 
        DateTime startAtUtc, 
        DateTime? endAtUtc, 
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new CreateEventCommand(
            id, title, description, location, startAtUtc, endAtUtc, new List<CreateEventCommand.TicketTypeRequest>()), cancellationToken);
    }

    public async Task CreateTicketTypeAsync(Guid ticketTypeId,
        Guid eventId, 
        string name, 
        decimal price, 
        string currency, 
        decimal quantity, 
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new CreateTicketTypeCommand(
            ticketTypeId, eventId, name, price, currency, quantity), cancellationToken);
    }
}
