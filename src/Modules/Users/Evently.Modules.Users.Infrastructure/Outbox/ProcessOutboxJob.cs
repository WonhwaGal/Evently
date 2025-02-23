using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Application.Data;
using Evently.Common.Domain;
using Evently.Common.Infrastructure.Serialization;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using Dapper;

namespace Evently.Modules.Users.Infrastructure.Outbox;

/// <summary>
/// Фоновая задача обработки исходящих сообщений
/// </summary>
[DisallowConcurrentExecution]   // Запретить параллельное выполнение задачи
                                // (если предыдущая задача еще не завершилась,
                                // новая не будет запущена)
internal sealed class ProcessOutboxJob(
    IDbConnectionFactory dbConnectionFactory,
    IServiceScopeFactory serviceScopeFactory,
    IOptions<OutboxOptions> outboxOptions,
    ILogger<ProcessOutboxJob> logger) : IJob
{
    private const string ModuleName = "Users";

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("{Module} - Beginning to process outbox messages", ModuleName);

        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        await using DbTransaction transaction = await connection.BeginTransactionAsync();

        // Получить исходящие сообщения
        IReadOnlyList<OutboxMessageResponse> outboxMessages = await GetOutboxMessagesAsync(connection, transaction);

        // Обработать исходящие сообщения
        foreach (OutboxMessageResponse outboxMessage in outboxMessages)
        {
            Exception? exception = null;
            try
            {
                IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    SerializerSettings.Instance)!;

                using IServiceScope scope = serviceScopeFactory.CreateScope();

                IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                await publisher.Publish(domainEvent);
            }
            catch (Exception caughtException)
            {
                logger.LogError(
                    caughtException,
                    "{Module} - Exception while processing outbox message {MessageId}",
                    ModuleName,
                    outboxMessage.Id);

                exception = caughtException;
            }

            // Обновить исходящее сообщение
            await UpdateOutboxMessageAsync(connection, transaction, outboxMessage, exception);
        }

        await transaction.CommitAsync();

        logger.LogInformation("{Module} - Completed processing outbox messages", ModuleName);
    }

    private async Task<IReadOnlyList<OutboxMessageResponse>> GetOutboxMessagesAsync(
        IDbConnection connection,
        IDbTransaction transaction)
    {
        string sql =
            $"""
             SELECT
                TOP ({outboxOptions.Value.BatchSize}) {nameof(OutboxMessageResponse.Id)},
                content AS {nameof(OutboxMessageResponse.Content)}
             FROM
                users.outbox_messages
             WHERE
                 processed_on_utc IS NULL
             ORDER BY
                 occurred_on_utc
             """;

        IEnumerable<OutboxMessageResponse> outboxMessages = await connection.QueryAsync<OutboxMessageResponse>(
            sql,
            transaction: transaction);

        return outboxMessages.ToList();
    }

    private async Task UpdateOutboxMessageAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        OutboxMessageResponse outboxMessage,
        Exception? exception)
    {
        const string sql =
            """
            UPDATE
                users.outbox_messages
            SET
                processed_on_utc = @ProcessedOnUtc,
                error = @Error
            WHERE
                id = @Id
            """;

        await connection.ExecuteAsync(
            sql,
            new
            {
                outboxMessage.Id,
                ProcessedOnUtc = DateTime.UtcNow,
                Error = exception?.ToString()
            },
            transaction: transaction);
    }

    internal sealed record OutboxMessageResponse(Guid Id, string Content);
}
