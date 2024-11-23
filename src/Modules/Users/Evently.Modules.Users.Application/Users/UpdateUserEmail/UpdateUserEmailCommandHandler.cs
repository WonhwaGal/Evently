using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Domain.Users;

namespace Evently.Modules.Users.Application.Users.UpdateUserEmail;
internal sealed class UpdateUserEmailCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserEmailCommand>
{
    public async Task<Result> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.Id, cancellationToken);

        if(user is null)
        {
            return Result.Failure<User>(UserErrors.NotFound(request.Id));
        }

        user.UpdateEmail(request.Email);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
