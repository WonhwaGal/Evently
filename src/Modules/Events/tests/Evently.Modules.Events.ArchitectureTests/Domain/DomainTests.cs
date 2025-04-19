using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Domain;
using Evently.Modules.Events.ArchitectureTests.Abstractions;
using NetArchTest.Rules;

namespace Evently.Modules.Events.ArchitectureTests.Domain;
public class DomainTests : BaseTest
{
    /// <summary>
    /// Check that all domain event classes are sealed
    /// </summary>
    [Fact]
    public void DomainEvents_Should_BeSealed()
    {
        Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Or()
            .Inherit(typeof(DomainEvent))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
    }
}
