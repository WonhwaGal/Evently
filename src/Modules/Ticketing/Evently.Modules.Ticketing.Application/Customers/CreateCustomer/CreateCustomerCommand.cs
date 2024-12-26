using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Customers.CreateCustomer;
public sealed record CreateCustomerCommand : ICommand
{
    public Guid CustomerId { get; init; }
    public string Email { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }

    public CreateCustomerCommand() { }

    public CreateCustomerCommand(Guid customerId, string email, string firstName, string lastName)
    {
        CustomerId = customerId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}
