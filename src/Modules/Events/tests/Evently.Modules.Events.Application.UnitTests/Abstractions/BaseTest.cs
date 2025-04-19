using Bogus;
using Evently.Modules.Events.Domain.Categories;

namespace Evently.Modules.Events.Application.UnitTests.Abstractions;

/// <summary>
/// Базовый абстрактный класс для тестов
/// </summary>
public abstract class BaseTest
{
    protected static readonly Faker Faker = new();

}
