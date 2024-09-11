using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Evently.Modules.Events.Application.Events.CreateEvent;
public sealed record CreateEventCommand (Guid CategoryId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc): IRequest<Guid>
{
}
