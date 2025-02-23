using Newtonsoft.Json;

namespace Evently.Common.Infrastructure.Serialization;
public static class SerializerSettings
{
    public static readonly JsonSerializerSettings Instance = new()
    {
        TypeNameHandling = TypeNameHandling.All, // Сохранять типы объектов в JSON
        MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead // Сохранять метаданные в начале JSON
    };
}
