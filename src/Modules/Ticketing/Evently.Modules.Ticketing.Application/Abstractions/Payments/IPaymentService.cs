namespace Evently.Modules.Ticketing.Application.Abstractions.Payments;

/// <summary>
/// Интерфейс описывает контракт взаимодействия с платежным шлюзом 
/// </summary>
public interface IPaymentService
{
    
    /// <summary>
    /// Провести оплату
    /// </summary>
    /// <param name="amount"> Сумма оплаты </param>
    /// <param name="currency"> Валюта оплаты </param>
    /// <returns></returns>
    Task<PaymentResponse> ChargeAsync(decimal amount, string currency);

    /// <summary>
    /// Провести возврат средств
    /// </summary>
    /// <param name="transactionId"> Идентификатор транзакции </param>
    /// <param name="amount"> Сумма возврата </param>
    /// <returns></returns>
    Task RefundAsync(Guid transactionId, decimal amount);
}
