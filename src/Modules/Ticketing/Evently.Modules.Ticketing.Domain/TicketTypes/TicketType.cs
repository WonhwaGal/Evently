using Evently.Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evently.Modules.Ticketing.Domain.TicketTypes;

public sealed class TicketType : Entity
{
    private TicketType()
    { 
    }

    public static TicketType Create(Guid ticketTypeId,
        Guid eventId,
        string name,
        decimal price,
        string currency,
        decimal quantity)
    {
        return new TicketType
        {
            Id = ticketTypeId,
            EventId = eventId,
            Name = name,
            Price = price,
            Currency = currency,
            Quantity = quantity
        };
    }

    public Guid Id { get; private set; }

    /// <summary>
    /// Идентификатор события
    /// </summary>
    public Guid EventId { get; private set; }

    /// <summary>
    /// Название типа билета
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Цена
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; private set; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string Currency { get; private set; }

    /// <summary>
    /// Количество
    /// </summary>
    [Column(TypeName = "decimal(6,0)")]
    public decimal Quantity { get; private set; }
}
