using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Messaging;
using Evently.Modules.Events.Domain.Abstractions;
using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateEventCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = Event.Create(
            request.CategoryId,
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc,
            request.EndsAtUtc);
        
        eventRepository.Insert(@event);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        //Result<Guid>.Success(@event.Id);
        return @event.Id;
    }
}
