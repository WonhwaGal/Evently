using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Domain.Categories.Events;
using Evently.Modules.Events.Domain.UnitTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Events.Domain.UnitTests.Categories;
public class CategoryTests : BaseTest
{
    /// <summary>
    /// Проверка поведения:
    /// Создание категории должно вызвать ошибку, если в названии нет букв
    /// </summary>
    [Theory]
    [InlineData("12345")]
    [InlineData("")]
    [InlineData("%43$$")]
    public void Create_ShouldReturnFailure_WhenNameHasNoLetters(string newName)
    {
        //Act
        Result<Category> result = Category.Create(newName);

        //Assert
        result.Error.Should().Be(CategoryErrors.IncorrectName);
    }

    /// <summary>
    /// Проверка поведения:
    /// Обновление названия категории должно вызвать ошибку, если в названии нет букв
    /// </summary>
    [Theory]
    [InlineData("987354")]
    [InlineData("")]
    [InlineData("%$_@")]
    public void ChangeName_ShouldReturnFailure_WhenNameHasNoLetters(string newName)
    {
        //Arrange
        Category category = Category.Create(Faker.Lorem.Word()).Value;

        //Act
        Result result = category.ChangeName(newName);

        //Assert
        result.Error.Should().Be(CategoryErrors.IncorrectName);
    }

    /// <summary>
    /// Проверка поведения:
    /// Перенесение категории в архив должно вызвать доменное событие
    /// </summary>
    [Fact]
    public void Archive_ShouldRaiseDomainEvent_WhenCategoryArchived()
    {
        // Arrange (подготовка)
        Category category = Category.Create(Faker.Music.Genre()).Value;

        // Act (действие)
        category.Archive();

        //Assert
        CategoryArchivedDomainEvent categoryArchivedDomainEvent = AssertDomainEventWasPublished<CategoryArchivedDomainEvent>(category);
        categoryArchivedDomainEvent.CategoryId.Should().Be(category.Id);
    }
}
