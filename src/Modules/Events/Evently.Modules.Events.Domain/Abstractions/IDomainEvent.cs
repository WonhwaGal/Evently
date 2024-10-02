using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Evently.Modules.Events.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    /// <summary>
    /// Идентификатор события
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Время возникновения события
    /// </summary>
    public DateTime OccurredOnUtc { get; }
}
