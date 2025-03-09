using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Ticketing.Infrastructure.Outbox;
/// <summary>
/// Параметры обработки исходящий сообщений
/// </summary>
internal sealed class OutboxOptions
{
    /// <summary>
    /// Интервал обработки исходящих сообщений
    /// </summary>
    public int IntervalInSeconds { get; set; }

    /// <summary>
    /// Размер пакета обработки исходящих сообщений
    /// </summary>
    public int BatchSize { get; set; }
}
