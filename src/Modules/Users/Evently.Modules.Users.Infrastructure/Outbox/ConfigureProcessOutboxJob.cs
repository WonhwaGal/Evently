using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Quartz;

namespace Evently.Modules.Users.Infrastructure.Outbox;

/// <summary>
/// Настройка фоновой обработки исходящих сообщений
/// </summary>
internal sealed class ConfigureProcessOutboxJob(
    IOptions<OutboxOptions> outboxOptions) : IConfigureOptions<QuartzOptions>
{
    // Параметры обработки исходящих сообщений
    private readonly OutboxOptions _outboxOptions = outboxOptions.Value;

    /// <summary>
    /// Настройка фоновой обработки исходящих сообщений
    /// </summary>
    /// <param name="options"> Параметры Quartz </param>
    public void Configure(QuartzOptions options)
    {
        // Имя фоновой задачи
        string jobName = typeof(ProcessOutboxJob).FullName!;
        // Добавить фоновую задачу обработки исходящих сообщений
        options
            .AddJob<ProcessOutboxJob>(configure => configure.WithIdentity(jobName)) // Добавить фоновую задачу
            .AddTrigger(configure => // Добавить триггер для фоновой задачи
                configure // Настроить триггер
                    .ForJob(jobName) // Для фоновой задачи
                    .WithSimpleSchedule(schedule => // С простым расписанием
                        schedule.WithIntervalInSeconds(_outboxOptions.IntervalInSeconds).RepeatForever())); // С интервалом в секундах, повторять бесконечно
    }
}

