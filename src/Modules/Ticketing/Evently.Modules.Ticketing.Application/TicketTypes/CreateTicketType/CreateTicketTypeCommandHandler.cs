using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Ticketing.Application.Abstractions.Data;
using Evently.Modules.Ticketing.Domain.TicketTypes;

//namespace Evently.Modules.Ticketing.Application.TicketTypes.CreateTicketType;
// internal sealed class CreateTicketTypeCommandHandler(
//     ITicketTypeRepository ticketTypeRepository,
//     IUnitOfWork unitOfWork) : ICommandHandler<CreateTicketTypeCommand>
// {
//     public async Task<Result> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
//     {
//         var ticketType = TicketType.Create(request.TicketTypeId,
//             request.EventId,
//             request.Name,
//             request.Price,
//             request.Currency,
//             request.Quantity);
//
//         ticketTypeRepository.Insert(ticketType);
//
//         await unitOfWork.SaveChangesAsync(cancellationToken);
//
//         return Result.Success();
//     }
// }
