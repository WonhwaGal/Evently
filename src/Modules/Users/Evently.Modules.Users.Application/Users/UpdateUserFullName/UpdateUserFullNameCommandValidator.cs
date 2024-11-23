using FluentValidation;

namespace Evently.Modules.Users.Application.Users.UpdateUserFullName;
internal sealed class UpdateUserFullNameCommandValidator : AbstractValidator<UpdateUserFullNameCommand>
{
    public UpdateUserFullNameCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Имя пользователя не должно быть пустым");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Фамилия пользователя не должна быть пустой");
    }
}
