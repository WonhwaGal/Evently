using System.Diagnostics.Metrics;

namespace Evently.Common.Infrastructure.Metrics;

public class AppMetrics
{
    private readonly Counter<long> _requestCounter;

    public AppMetrics(Meter meter)
    {
        _requestCounter = meter.CreateCounter<long>(
            "app.requests.total",
            description: "Общее количество обработанных запросов");
    }

    public void IncrementRequestCount()
    {
        _requestCounter.Add(1);
    }
}
