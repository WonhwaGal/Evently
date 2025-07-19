namespace Evently.Modules.Ticketing.Application.Abstractions.Payments;

/// <summary>
/// Ответ от платежного шлюза
/// </summary>
/// <param name="TransactionId"> Идентификатор транзакции </param>
/// <param name="Amount"> Сумма оплаты </param>
/// <param name="Currency"> Валюта оплаты </param>
public sealed record PaymentResponse(Guid TransactionId, decimal Amount, string Currency);
