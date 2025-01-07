using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Common.Domain;
public abstract class DomainEvent: IDomainEvent
{
    protected DomainEvent()
    {
        DomainEventId = Guid.NewGuid();
        OccurredOnUtc = DateTime.UtcNow;
    }

    public Guid DomainEventId { get; init; }

    public DateTime OccurredOnUtc { get; init; }
}
