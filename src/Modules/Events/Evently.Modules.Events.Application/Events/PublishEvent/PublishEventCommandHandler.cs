using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using MediatR;

namespace Evently.Modules.Events.Application.Events.PublishEvent;
public sealed class PublishEventCommandHandler(
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<PublishEventCommand>
{
    public async Task Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        eventRepository.PublishEvent(request.EventId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
