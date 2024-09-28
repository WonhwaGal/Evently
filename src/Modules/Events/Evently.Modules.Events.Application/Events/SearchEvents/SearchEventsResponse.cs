using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Events.GetEvent;

namespace Evently.Modules.Events.Application.Events.SearchEvents;
public sealed record SearchEventsResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<EventResponse> Events);
