using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Ticketing.PublicApi;
using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Domain.Users;

namespace Evently.Modules.Users.Application.Users.RegisterUser;
internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    //ITicketingApi ticketingApi,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Email,
            request.Name,
            request.LastName);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        //await ticketingApi.CreateCustomerAsync(
        //    user.Id,
        //    request.Email,
        //    request.Name,
        //    request.LastName,
        //    cancellationToken);

        return user.Id;
    }
}
