
namespace Evently.Common.Application.Clock;

/// <summary>
/// Контракт описывающий поставщика текущего времени
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Возвращает текущее время в UTC
    /// </summary>
    DateTime UtcNow { get; }
}
