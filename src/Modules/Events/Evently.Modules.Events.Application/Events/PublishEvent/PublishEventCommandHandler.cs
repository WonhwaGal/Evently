using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Messaging;
using Evently.Modules.Events.Domain.Abstractions;
using Evently.Modules.Events.Domain.Events;
using MediatR;

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

        @event.UpdateStatus(EventStatus.Published);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
