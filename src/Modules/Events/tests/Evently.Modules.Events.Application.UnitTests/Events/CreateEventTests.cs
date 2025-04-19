using Bogus;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.UnitTests.Abstractions;
using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Domain.Events;
using FluentAssertions;
using Evently.Common.Application.Clock;
using NSubstitute;

namespace Evently.Modules.Events.Application.UnitTests.Events;

public class CreateEventTests : BaseTest
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;

    private readonly CreateEventCommandHandler _handler;
    private readonly ICategoryRepository _categoryRepositoryMock;
    private readonly IEventRepository _eventRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    //ctor that makes preparations for the tests
    public CreateEventTests()
    {
        _categoryRepositoryMock = Substitute.For<ICategoryRepository>();
        _eventRepositoryMock = Substitute.For<IEventRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        IDateTimeProvider? dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        //NSubstitute function
        dateTimeProviderMock.UtcNow.Returns(UtcNow);

        _handler = new CreateEventCommandHandler(
            _eventRepositoryMock,
            _categoryRepositoryMock,
            _unitOfWorkMock,
            dateTimeProviderMock);
    }

    /// <summary>
    /// Проверка поведения:
    /// Обработчик команды на создание мероприятия должен вернуть ошибку,
    /// если дата мероприятия в прошлом.
    /// </summary>
    [Fact]
    public async Task Handle_Should_ReturnStartDateInPast_WhenDateIsInPast()
    {
        //! Arrange
        Result<Category> createCategoryResult = Category.Create(Faker.Music.Genre());

        CreateEventCommand command = new(
            CategoryId: createCategoryResult.Value.Id,
            Title: "Title",
            Description: "Description",
            Location: "Location",
            StartsAtUtc: UtcNow.AddDays(-1),
            EndsAtUtc: null
        );

        // Act
        Result<Guid> result = await _handler.Handle(command, CancellationToken.None);

        // Assert (утверждение)
        result.Error.Should().Be(EventErrors.StartDateInPast);
    }

    /// <summary>
    /// Проверка поведения:
    /// Обработчик команды на создание мероприятия должен вернуть ошибку,
    /// если указанная категория не найдена.
    /// </summary>
    [Fact]
    public async Task Handle_Should_CategoryErrorNotFound_WhenCategoryIsNull()
    {
        //Arrange
        Result<Category> createCategoryResult = Category.Create(Faker.Music.Genre());

        CreateEventCommand command = new(
            CategoryId: createCategoryResult.Value.Id,
            Title: "Title",
            Description: "Description",
            Location: "Location",
            StartsAtUtc: UtcNow,
            EndsAtUtc: null
        );

        _categoryRepositoryMock
            .GetByIdAsync(createCategoryResult.Value.Id, Arg.Any<CancellationToken>())
            .Returns((Category)null);

        //Act
        Result<Guid> result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Error.Should().Be(CategoryErrors.NotFound(createCategoryResult.Value.Id));
    }

    /// <summary>
    /// Проверка поведения:
    /// Обработчик команды на создание мероприятия должен вернуть ошибку,
    /// если указанная категория уже помещена в архив.
    /// </summary>
    [Fact]
    public async Task Handle_Should_ReturnCategoryIsArchived_WhenCategoryIsArchived()
    {
        //Arrange
        Result<Category> createCategoryResult = Category.Create(Faker.Music.Genre());
        createCategoryResult.Value.Archive();

        CreateEventCommand command = new(
            CategoryId: createCategoryResult.Value.Id,
            Title: "Title",
            Description: "Description",
            Location: "Location",
            StartsAtUtc: UtcNow,
            EndsAtUtc: null
        );

        _categoryRepositoryMock
            .GetByIdAsync(createCategoryResult.Value.Id, Arg.Any<CancellationToken>())
            .Returns(createCategoryResult.Value);

        //Act
        Result<Guid> result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Error.Should().Be(CategoryErrors.IsArchived(createCategoryResult.Value.Id));
    }

    /// <summary>
    /// Проверка поведения:
    /// Обработчик команды на создание мероприятия должен подтвердить 
    /// корректную передачу мероприятия в базу данных
    /// </summary>
    [Fact]
    public async Task Handle_Should_CallRepository_WhenEventIsAdded()
    {
        //Arrange
        Result<Category> createCategoryResult = Category.Create(Faker.Music.Genre());

        CreateEventCommand command = new(
            CategoryId: createCategoryResult.Value.Id,
            Title: "Title",
            Description: "Description",
            Location: "Location",
            StartsAtUtc: UtcNow,
            EndsAtUtc: null
        );

        _categoryRepositoryMock
            .GetByIdAsync(createCategoryResult.Value.Id, Arg.Any<CancellationToken>())
            .Returns(createCategoryResult.Value);

        //Act
        Result<Guid> result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        _eventRepositoryMock  // making sure that the following happened in the Handle (in act section)
            .Received(1)  //how many times to call method
            .Insert(Arg.Is<Event>(e => e.Id == result.Value));  // 1) what method to call and what argument to put in it
    }                                                           // 2) make sure the resulting Id is corrent
}
