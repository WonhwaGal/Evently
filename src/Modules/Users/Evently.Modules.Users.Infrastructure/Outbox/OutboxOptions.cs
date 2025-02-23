using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Users.Infrastructure.Outbox;

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
    /// (количество сообщений, которые будут обработаны за один
    /// запуск фоновой обработки)
    /// </summary>
    public int BatchSize { get; set; }
}
