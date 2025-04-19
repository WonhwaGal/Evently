using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.CancelEvent;
using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Events.IntegrationTests.Events;
public class CancelEventTests : BaseIntegrationTest
{
    public CancelEventTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Cancel_Should_ReturnFailure_WhenEventNotFound()
    {
        var id = Guid.NewGuid();
        var cancelCommand = new CancelEventCommand(id);

        Result result = await Sender.Send(cancelCommand);

        result.Error.Should().Be(EventErrors.NotFound(id));
    }

}
