using FluentValidation;

namespace Evently.Modules.Users.Application.Users.UpdateUserEmail;
internal sealed class UpdateUserEmailCommandValidator : AbstractValidator<UpdateUserEmailCommand>
{
    public UpdateUserEmailCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Идентификатор пользователя не должен быть пустым");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Почтовый адрес пользователя не должен быть пустым")
            .EmailAddress().WithMessage("Указанное в поле не является почтовым адресом");
    }
}
