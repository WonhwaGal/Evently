using Bogus;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.Events.Events;
using Evently.Modules.Events.Domain.UnitTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Events.Domain.UnitTests.Events;
public class EventTests : BaseTest
{
    /// <summary>
    /// Проверка поведения:
    /// Создание мероприятия должно вызвать ошибку если дата старта позже даты окончания
    /// </summary>
    [Fact]
    public void Create_ShouldReturnFailure_WhenEndDatePrecedesStartDate()
    {
        // Arrange (подготовка)
        Category category = Category.Create(Faker.Music.Genre()).Value;
        DateTime startsAtUtc = DateTime.UtcNow;
        DateTime endsAtUtc = startsAtUtc.AddMinutes(-1);

        // Act (действие)
        Result<Event> result = Event.Create(
            category.Id,
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            startsAtUtc,
            endsAtUtc);

        // Assert (утверждение)
        //Assert.Equal(result.Error, EventErrors.EndDatePrecedesStartDate);
        result.Error.Should().Be(EventErrors.EndDatePrecedesStartDate);
    }

    /// <summary>
    /// Проверка поведения:
    /// Создание мероприятия должно вызвать событие EventCreatedDomainEvent
    /// </summary>
    [Fact]
    public void Create_ShouldRaiseDomainEvent_WhenEventCreated()
    {
        // Arrange (подготовка)
        Category category = Category.Create(Faker.Music.Genre()).Value;
        DateTime startsAtUtc = DateTime.UtcNow;

        // Act (действие)
        Result<Event> result = Event.Create(
            category.Id,
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            startsAtUtc,
            null);

        Event @event = result.Value;

        //Assert.NotNull(domainEvent);
        //Assert.Equal(@event.Id, domainEvent.EventId);
        //domainEvent!.EventId.Should().Be(@event.Id);
        EventCreatedDomainEvent eventCreatedDomainEvent = AssertDomainEventWasPublished<EventCreatedDomainEvent>(@event);
        eventCreatedDomainEvent.EventId.Should().Be(@event.Id);
    }

    /// <summary>
    /// Проверка поведения:
    /// При публикации мероприятия, если мероприятие находится
    /// не в начальном состоянии, должна вернуться ошибка NotDraft
    /// </summary>
    [Fact]
    public void Publish_ShouldReturnFailure_WhenEventNotDraft()
    {
        //Arrange (подготовка)
        Category category = Category.Create(Faker.Music.Genre()).Value;
        DateTime startsAtUtc = DateTime.UtcNow;

        Event @event = CreateCorrentEvent();

        // Опубликовать мероприятие
        @event.Publish();

        //Act (действие)
        // Повторно опубликовать мероприятие, которое уже опубликовано
        Result publishResult = @event.Publish();

        //Assert (утверждение)
        publishResult.Error.Should().Be(EventErrors.NotDraft);
    }
}
