using FluentValidation;

namespace Evently.Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Почтовый адрес пользователя не должен быть пустым")
            .EmailAddress().WithMessage("Указанное в поле не является почтовым адресом");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя пользователя не должно быть пустым");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Фамилия пользователя не должна быть пустой");
    }
}
