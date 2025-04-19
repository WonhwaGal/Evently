using Bogus;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.TicketTypes.CreateTicketType;
using Evently.Modules.Events.Application.UnitTests.Abstractions;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.TicketTypes;
using FluentAssertions;
using NSubstitute;

namespace Evently.Modules.Events.Application.UnitTests.TicketTypes;
public class CreateTicketTypeTests : BaseTest
{
    private readonly CreateTicketTypeCommandHandler _handler;
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTicketTypeTests()
    {
        _ticketTypeRepository = Substitute.For<ITicketTypeRepository>();
        _eventRepository = Substitute.For<IEventRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CreateTicketTypeCommandHandler(
            _ticketTypeRepository,
            _eventRepository,
            _unitOfWork);
    }

    /// <summary>
    /// Проверка поведения:
    /// Обработчик команды на создание типа билета должен вернуть ошибку,
    /// если указано некорректное ID мероприятия
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Handle_Should_ReturnEventNotFound_WhenEventIsWrong()
    {
        //Arrange
        var eventId = Guid.NewGuid();
        CreateTicketTypeCommand command = new(
            eventId,
            Faker.Lorem.Word(),
            Faker.Random.Decimal(1,100),
            Faker.Finance.Currency().Code,
            Faker.Random.Decimal(1, 100));

        //Act
        Result<Guid> result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Error.Should().Be(EventErrors.NotFound(eventId));
    }
}
