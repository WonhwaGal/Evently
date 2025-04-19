using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Categories.CreateCategory;
using Evently.Modules.Events.Application.UnitTests.Abstractions;
using Evently.Modules.Events.Domain.Categories;
using FluentAssertions;
using NSubstitute;

namespace Evently.Modules.Events.Application.UnitTests.Categories;
public class CreateCategoryTests : BaseTest
{
    private readonly CreateCategoryCommandHandler _handler;
    private readonly ICategoryRepository _categoryRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public CreateCategoryTests()
    {
        _categoryRepositoryMock = Substitute.For<ICategoryRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new CreateCategoryCommandHandler(
            _categoryRepositoryMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task CreateCategory_Should_ReturnAlreadyExists_WhenCategoryExists()
    {
        //Arrange
        Category newCategory = Category.Create(Faker.Music.Genre()).Value;
        var command = new CreateCategoryCommand(newCategory.Name);

        _categoryRepositoryMock
            .GetByNameAsync(newCategory.Name, Arg.Any<CancellationToken>())
            .Returns(newCategory);

        //Act
        Result<Guid> result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Error.Should().Be(CategoryErrors.AlreadyExists(newCategory.Name));
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenCategoryNameIsIncorrect()
    {
        //Arrange
        Category newCategory = Category.Create(Faker.Music.Genre()).Value;
        var command = new CreateCategoryCommand("123*%3");

        _categoryRepositoryMock
            .GetByNameAsync(newCategory.Name, Arg.Any<CancellationToken>())
            .Returns((Category)null);

        //Act
        Result<Guid> result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Error.Should().Be(CategoryErrors.IncorrectName);
    }

    [Fact]
    public async Task Handle_Should_CallRepository_WhenCategoryIsAdded()
    {
        //Arrange
        Category newCategory = Category.Create(Faker.Music.Genre()).Value;
        var command = new CreateCategoryCommand(newCategory.Name);

        _categoryRepositoryMock
            .GetByNameAsync(newCategory.Name, Arg.Any<CancellationToken>())
            .Returns((Category)null);

        //Act
        Result<Guid> result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        _categoryRepositoryMock
            .Received(1)
            .Insert(Arg.Is<Category>(c => c.Id == result.Value));
    }
}
