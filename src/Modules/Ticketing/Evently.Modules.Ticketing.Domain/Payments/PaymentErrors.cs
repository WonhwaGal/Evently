using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Payments;

/// <summary>
/// Справочник ошибок сущности "Платеж" (Payment)
/// </summary>
public static class PaymentErrors
{
    
    /// <summary>
    /// Ошибка: платеж с указанным идентификатором не найден
    /// </summary>
    /// <param name="paymentId"> Идентификатор платежа </param>
    /// <returns></returns>
    public static Error NotFound(Guid paymentId) =>
        Error.NotFound("Payments.NotFound", $"The payment with the identifier {paymentId} was not found");

    /// <summary>
    /// Ошибка: по платежу уже был произведен возврат
    /// </summary>
    public static readonly Error AlreadyRefunded =
        Error.Problem("Payments.AlreadyRefunded", "The payment was already refunded");

    /// <summary>
    /// Ошибка: недостаточно средств для возврата
    /// </summary>
    public static readonly Error NotEnoughFunds =
        Error.Problem("Payments.NotEnoughFunds", "There are not enough funds for a refund");
    
}
