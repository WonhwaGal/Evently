using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.TicketTypes;

namespace Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
internal sealed class UpdateTicketTypePriceCommandHandler(
    ITicketTypeRepository ticketTypeRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateTicketTypePriceCommand>
{
    public async Task<Result> Handle(UpdateTicketTypePriceCommand request, CancellationToken cancellationToken)
    {
        TicketType? ticketType = await ticketTypeRepository.GetAsync(request.TicketId, cancellationToken);

        if(ticketType is null)
        {
            return Result.Failure(TicketTypeErrors.NotFound(request.TicketId));
        }

        ticketType.UpdatePrice(request.NewPrice);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
