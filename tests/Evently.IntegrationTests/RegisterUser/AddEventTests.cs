using Evently.Common.Domain;
using Evently.IntegrationTests.Abstractions;
using Evently.Modules.Events.Application.Categories.CreateCategory;
using Evently.Modules.Events.Application.Events.CreateEvent;
//using Evently.Modules.Ticketing.Application.Events.GetEvent;
using FluentAssertions;

namespace Evently.IntegrationTests.RegisterUser;
public class AddEventTests : BaseIntegrationTest
{
    public AddEventTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    /// <summary>
    /// Тест проверяет, создается ли event в модуле Ticketing при
    /// добавлении мероприятия в модуле Events
    /// </summary>
    /// <returns></returns>
    //[Fact]
    //public async Task AddEvent_Should_PropagateToTicketingModule()
    //{
    //    var createCategoryCommand = new CreateCategoryCommand(Faker.Music.Genre());
    //    Result<Guid> categoryResult = await Sender.Send(createCategoryCommand);

    //    var command = new CreateEventCommand(
    //        categoryResult.Value,
    //        Faker.Random.Word(),
    //        Faker.Random.Words(8),
    //        Faker.Address.StreetAddress(),
    //        DateTime.UtcNow.AddDays(5),
    //        null
    //        );

    //    Result<Guid> result = await Sender.Send(command);
    //    result.IsSuccess.Should().BeTrue();

    //    Result<EventResponse> eventResult = await Poller.WaitAsync(
    //        TimeSpan.FromSeconds(30),
    //        async () =>
    //        {
    //            var query = new GetEventQuery(result.Value);
    //            Result<EventResponse> eventResponseResult = await Sender.Send(query);
    //            return eventResponseResult;
    //        });

    //    eventResult.IsSuccess.Should().BeTrue();
    //    eventResult.Value.Should().NotBeNull();
    //}
}
