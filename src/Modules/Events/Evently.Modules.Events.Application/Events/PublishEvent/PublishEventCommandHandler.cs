using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.PublishEvent;
public sealed class PublishEventCommandHandler(
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<PublishEventCommand>
{
    public async Task<Result> Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        if(@event.StartsAtUtc <= DateTime.UtcNow)
        {
            return Result.Failure(EventErrors.StartDateInPast);
        }

        if (@event.EndsAtUtc <= DateTime.UtcNow)
        {
            return Result.Failure(EventErrors.StartDateInPast);
        }

        @event.Publish();
        //@event.UpdateStatus(EventStatus.Published);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
