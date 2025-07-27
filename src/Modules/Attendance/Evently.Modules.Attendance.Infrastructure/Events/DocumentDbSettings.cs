namespace Evently.Modules.Attendance.Infrastructure.Events;

/// <summary>
/// Настройки для работы с базой данных MongoDB в модуле Attendance.
/// </summary>
internal static class DocumentDbSettings
{
    /// <summary>
    /// Имя базы данных MongoDB, используемой в модуле Attendance.
    /// </summary>
    internal const string Database = "attendance";

    /// <summary>
    /// Коллекция MongoDB, в которой хранятся статистические данные
    /// о мероприятиях (EventStatistics).
    /// </summary>
    internal const string EventStatistics = "event-statistics";
}
