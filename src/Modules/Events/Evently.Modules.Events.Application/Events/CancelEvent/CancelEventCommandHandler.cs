using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using MediatR;

namespace Evently.Modules.Events.Application.Events.CancelEvent;

public sealed class CancelEventCommandHandler(
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CancelEventCommand>
{
    public async Task Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        eventRepository.CancelEvent(request.EventId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
