using Evently.Modules.Events.ArchitectureTests.Abstractions;
using MediatR;
using NetArchTest.Rules;

namespace Evently.Modules.Events.ArchitectureTests.Application;
public class ApplicationTests : BaseTest
{
    [Fact]
    public void QueriesAndCommands_Should_BeSealed()
    {
        Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IBaseRequest))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
    }
}
