using Bogus;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Events.IntegrationTests.Events;
public class CreateEventTests : BaseIntegrationTest
{
    public CreateEventTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenStartDateInPast()
    {
        var command = new CreateEventCommand(
            Guid.NewGuid(),
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            DateTime.UtcNow.AddMinutes(-1),
            null);

        Result<Guid> result = await Sender.Send(command);

        result.Error.Should().Be(EventErrors.StartDateInPast);
    }
}
