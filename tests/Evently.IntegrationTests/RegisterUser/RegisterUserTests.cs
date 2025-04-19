using Evently.Common.Domain;
using Evently.IntegrationTests.Abstractions;
using Evently.Modules.Ticketing.Application.Customers.GetCustomer;
using Evently.Modules.Users.Application.Users.RegisterUser;
using FluentAssertions;

namespace Evently.IntegrationTests.RegisterUser;
public class RegisterUserTests : BaseIntegrationTest
{
    public RegisterUserTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    /// <summary>
    /// Тест проверяет, создается ли customer в модуле Ticketing при
    /// создании пользователя в модуле Users
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task RegisterUser_Should_PropagateToTicketingModule()
    {
        var command = new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Internet.Password(6),
            Faker.Name.FirstName(),
            Faker.Name.LastName());
        Result<Guid> userResult = await Sender.Send(command);

        // making sure that user is created
        userResult.IsSuccess.Should().BeTrue();

        Result<CustomerResponse> customerResult = await Poller.WaitAsync(
            TimeSpan.FromSeconds(30),
            async () =>
            {
                var query = new GetCustomerQuery(userResult.Value);
                Result<CustomerResponse> customerResult = await Sender.Send(query);
                return customerResult;
            });

        // NOT CORRECT CHECK !!! because it will finish before inbox/outbox will not have time to handle
        //var query = new GetCustomerQuery(userResult.Value);
        //Result<CustomerResponse> customerResult = await Sender.Send(query);

        customerResult.IsSuccess.Should().BeTrue();
        customerResult.Value.Should().NotBeNull();
    }
}
