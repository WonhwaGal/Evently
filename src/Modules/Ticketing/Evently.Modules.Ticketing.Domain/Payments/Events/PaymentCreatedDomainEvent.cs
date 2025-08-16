using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Payments.Events;

public sealed class PaymentCreatedDomainEvent(Guid paymentId) : DomainEvent
{
    public Guid PaymentId { get; init; } = paymentId;
}
