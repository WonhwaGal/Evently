using System.Reflection;
using Evently.ArchitectureTests.Abstractions;
using Evently.Modules.Events.ArchitectureTests.Abstractions;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Infrastructure;
using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.Infrastructure;
using FluentAssertions;
using NetArchTest.Rules;

namespace Evently.ArchitectureTests.Layers;

public class ModuleTests : BaseTest
{
    [Fact]
    public void UsersModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [EventsNamespace, TicketingNamespace];
        string[] integrationEventsModules =
        [
            EventsIntegrationEventsNamespace
        ];

        List<Assembly> usersAssemblies =
        [
            typeof(User).Assembly,
            Modules.Users.Application.AssemblyReference.Assembly,
            Modules.Users.Presentation.AssemblyReference.Assembly,
            typeof(UsersModule).Assembly
        ];

        Types.InAssemblies(usersAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .FailingTypes?.
            Should().
            BeEmpty();
    }

    [Fact]
    public void EventsModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherNodules = [UsersNamespace, TicketingNamespace];
        string[] integrationEventsModules = 
        [
            UsersIntegrationEventsNamespace
        ];

        List<Assembly> eventsAssemblies = 
        [
            typeof(Event).Assembly,
            Modules.Events.Application.AssemblyReference.Assembly,
            Modules.Events.Presentation.AssemblyReference.Assembly,
            typeof(EventsModule).Assembly
        ];

        Types.InAssemblies(eventsAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherNodules)
            .GetResult()
            .ShouldBeSuccessful();
    }
}
