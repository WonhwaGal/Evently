using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Messaging;
using Evently.Modules.Events.Domain.Abstractions;
using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.CancelEvent;

public sealed class CancelEventCommandHandler(
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CancelEventCommand>
{
    public async Task<Result> Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        if(@event.StartsAtUtc <= DateTime.Now)
        {
            return Result.Failure(EventErrors.StartDateInPast);
        }

        @event.UpdateStatus(EventStatus.Cancelled);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
