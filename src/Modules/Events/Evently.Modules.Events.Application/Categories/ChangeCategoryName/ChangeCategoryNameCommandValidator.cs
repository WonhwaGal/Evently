using FluentValidation;

namespace Evently.Modules.Events.Application.Categories.ChangeCategoryName;

internal sealed class ChangeCategoryNameCommandValidator : AbstractValidator<ChangeCategoryNameCommand>
{
    public ChangeCategoryNameCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Идентификатор категории не должен быть пустым");
        RuleFor(x => x.NewName)
            .NotEmpty().WithMessage("Название категории не должно быть пустым")
            .MaximumLength(25).WithMessage("Название категории не должно иметь более 25 символов");
    }
}
