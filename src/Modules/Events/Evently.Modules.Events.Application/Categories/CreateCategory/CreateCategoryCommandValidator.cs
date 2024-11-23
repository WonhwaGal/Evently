using FluentValidation;

namespace Evently.Modules.Events.Application.Categories.CreateCategory;
internal sealed class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotEmpty().WithMessage("Название категории не должно быть пустым")
            .MaximumLength(25).WithMessage("Название категории не должно иметь более 25 символов");
    }
}
