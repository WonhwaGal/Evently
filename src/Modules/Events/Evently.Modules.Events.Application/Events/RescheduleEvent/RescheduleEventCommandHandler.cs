using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using MediatR;

namespace Evently.Modules.Events.Application.Events.RescheduleEvent;

public sealed class RescheduleEventCommandHandler(
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RescheduleEventCommand>
{
    public async Task Handle(RescheduleEventCommand request, CancellationToken cancellationToken)
    {
        eventRepository.Reschedule(request.EventId, request.StartsAtUtc, request.EndsAtUtc);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
