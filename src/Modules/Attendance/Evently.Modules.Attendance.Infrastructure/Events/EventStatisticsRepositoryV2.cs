using Evently.Modules.Attendance.Domain.Events;
using MongoDB.Driver;

namespace Evently.Modules.Attendance.Infrastructure.Events;

internal sealed class EventStatisticsRepositoryV2 : IEventStatisticsRepositoryV2
{
    private readonly IMongoCollection<EventStatisticsV2> _collection;

    public EventStatisticsRepositoryV2(IMongoClient mongoClient)
    {
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(DocumentDbSettings.Database);

        _collection = mongoDatabase.GetCollection<EventStatisticsV2>(DocumentDbSettings.EventStatistics);
    }

    public async Task<EventStatisticsV2> GetAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        FilterDefinition<EventStatisticsV2> filter = Builders<EventStatisticsV2>.Filter.Eq(e => e.EventId, eventId);

        EventStatisticsV2 eventStatistics = await _collection.Find(filter).SingleAsync(cancellationToken);

        return eventStatistics;
    }

    public async Task InsertAsync(EventStatisticsV2 eventStatistics, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(eventStatistics, cancellationToken: cancellationToken);
    }

    public async Task ReplaceAsync(EventStatisticsV2 eventStatistics, CancellationToken cancellationToken = default)
    {
        FilterDefinition<EventStatisticsV2> filter = Builders<EventStatisticsV2>
            .Filter
            .Eq(e => e.EventId, eventStatistics.EventId);

        await _collection.ReplaceOneAsync(filter, eventStatistics, cancellationToken: cancellationToken);
    }
}
