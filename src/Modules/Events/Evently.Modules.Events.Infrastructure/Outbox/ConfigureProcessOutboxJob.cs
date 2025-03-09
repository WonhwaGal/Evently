using Microsoft.Extensions.Options;
using Quartz;

namespace Evently.Modules.Events.Infrastructure.Outbox;
/// <summary>
/// Настройка фоновой обработки исходящих сообщений
/// </summary>
internal sealed class ConfigureProcessOutboxJob(
    IOptions<OutboxOptions> outboxOptions) : IConfigureOptions<QuartzOptions>
{
    private readonly OutboxOptions _outboxOptions = outboxOptions.Value;

    /// <summary>
    /// Настройка фоновой обработки исходящих сообщений
    /// </summary>
    /// <param name="options"> Параметры Quartz </param>
    public void Configure(QuartzOptions options)
    {
        string jobName = typeof(ProcessOutboxJob).FullName!;

        options
            .AddJob<ProcessOutboxJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(_outboxOptions.IntervalInSeconds).RepeatForever()));
    }
}
