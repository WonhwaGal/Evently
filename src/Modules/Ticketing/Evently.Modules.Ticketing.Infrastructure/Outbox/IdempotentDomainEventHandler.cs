using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Common.Infrastructure.Outbox;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace Evently.Modules.Ticketing.Infrastructure.Outbox;

internal sealed class IdempotentDomainEventHandler<TDomainEvent>(
    IDomainEventHandler<TDomainEvent> decorated,
    IDbConnectionFactory dbConnectionFactory,
    ILogger<IdempotentDomainEventHandler<TDomainEvent>> logger)
    : DomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public override async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        var outboxMessageConsumer = new OutboxMessageConsumer(domainEvent.DomainEventId, decorated.GetType().Name);

        if (await OutboxConsumerExistsAsync(connection, outboxMessageConsumer))
        {
            return;
        }

        await decorated.Handle(domainEvent, cancellationToken);

        await InsertOutboxConsumerAsync(connection, outboxMessageConsumer);
        logger.LogInformation("Outbox consumer {ConsumerName} for outbox message {OutboxMessageId} has been inserted",
            outboxMessageConsumer.Name, outboxMessageConsumer.OutboxMessageId);
    }

    private static async Task<bool> OutboxConsumerExistsAsync(
        DbConnection dbConnection,
        OutboxMessageConsumer outboxMessageConsumer)
    {
        const string sql =
            """
        SELECT CASE
            WHEN EXISTS (
                SELECT 1
                FROM ticketing.outbox_message_consumers
                WHERE outbox_message_id = @OutboxMessageId AND
                      name = @Name
            )
            THEN 1
            ELSE 0
        END as RecordExists
        """;

        return await dbConnection.ExecuteScalarAsync<bool>(sql, outboxMessageConsumer);
    }

    private static async Task InsertOutboxConsumerAsync(
        DbConnection dbConnection,
        OutboxMessageConsumer outboxMessageConsumer)
    {
        const string sql =
            """
        INSERT INTO ticketing.outbox_message_consumers(outbox_message_id, name)
        VALUES (@OutboxMessageId, @Name)
        """;

        await dbConnection.ExecuteAsync(sql, outboxMessageConsumer);
    }
}
