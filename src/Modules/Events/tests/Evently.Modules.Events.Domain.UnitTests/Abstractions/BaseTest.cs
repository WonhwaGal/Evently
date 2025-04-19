using Bogus;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Domain.UnitTests.Abstractions;
/// <summary>
/// Базовый абстрактный класс для тестов
/// </summary>
public abstract class BaseTest
{
    protected static readonly Faker Faker = new();

    /// <summary>
    /// Проверить, что событие домена было опубликовано
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <typeparam name="T">Тип события домена</typeparam>
    /// <returns>Событие домена</returns>
    /// <exception cref="Exception">Ошибка, указывающая на то, что событие домена не было опубликовано</exception>
    public static T AssertDomainEventWasPublished<T>(Entity entity)
        where T : IDomainEvent
    {
        T? domainEvent = entity.DomainEvents.OfType<T>().SingleOrDefault();

        if (domainEvent is null)
        {
            throw new Exception($"{typeof(T).Name} was not published");
        }

        return domainEvent;
    }

    /// <summary>
    /// Создать корректное мероприятие
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    public static Event CreateCorrentEvent()
    {
        Category category = Category.Create(Faker.Music.Genre()).Value;
        DateTime startsAtUtc = DateTime.UtcNow;

        Result<Event> result = Event.Create(
            category.Id,
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            startsAtUtc,
            null);

        return result.Value;
    }
}
