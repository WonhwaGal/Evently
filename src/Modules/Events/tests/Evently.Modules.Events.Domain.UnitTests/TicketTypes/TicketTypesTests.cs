using Bogus;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.Domain.TicketTypes.TicketTypes;
using Evently.Modules.Events.Domain.UnitTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Events.Domain.UnitTests.TicketTypes;
public class TicketTypesTests : BaseTest
{
    /// <summary>
    /// Проверка поведения:
    /// Обновление цены типа билета должно вызвать ошибку при некорректной цене
    /// </summary>
    [Theory]
    [InlineData(5.152)]
    [InlineData(-200)]
    [InlineData(0)]
    public void Update_ShouldReturnFailure_WhenPriceUpdated(decimal price)
    {
        //Arrange
        var ticketType = TicketType.Create(
            CreateCorrentEvent(),
            Faker.Lorem.Word(),
            Faker.Random.Decimal(100, 2000),
            Faker.Finance.Currency().Code,
            Faker.Random.Decimal(10, 1000));

        //Act
        Result result = ticketType.UpdatePrice(price);

        //Assert
        result.Error.Should().Be(TicketTypeErrors.IncorrentPrice);
    }

    /// <summary>
    /// Проверка поведения:
    /// Создание типа билета должно вызвать событие TicketTypeCreatedDomainEvent
    /// </summary>
    [Fact]
    public void Create_ShouldRaiseDomainEvent_WhenTicketTypeCreated()
    {
        //Arrange
        Event @event = CreateCorrentEvent();

        //Act
        var ticketType = TicketType.Create(
            @event,
            Faker.Lorem.Word(),
            Faker.Random.Decimal(50,2000),
            Faker.Finance.Currency().Code,
            Faker.Random.Decimal(10, 1000));

        TicketTypeCreatedDomainEvent domainEvent =
            AssertDomainEventWasPublished<TicketTypeCreatedDomainEvent>(ticketType);

        //Assert
        domainEvent.TicketTypeId.Should().Be(ticketType.Id);
    }
}
