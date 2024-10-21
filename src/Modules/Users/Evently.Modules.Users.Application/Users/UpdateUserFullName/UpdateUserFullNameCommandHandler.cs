using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Domain.Users;

namespace Evently.Modules.Users.Application.Users.UpdateUserName;
internal sealed class UpdateUserFullNameCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserFullNameCommand>
{
    public async Task<Result> Handle(UpdateUserFullNameCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.Id, cancellationToken);

        if(user is null)
        {
            return Result.Failure<User>(UserErrors.NotFound(request.Id));
        }

        user.UpdateUserName(request.FirstName, request.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
