using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Evently.Modules.Events.Application.Events.GetEvent;
public sealed record GetEventQuery(Guid EventId) : IRequest<EventResponse?>
{
}
