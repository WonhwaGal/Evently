using FluentValidation;

namespace Evently.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Идентификатор категории события не должен быть пустым");
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Наименование события не должно быть пустым");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Описание события не должно быть пустым");
        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Место проведения события не должно быть пустым");
        RuleFor(x => x.StartsAtUtc)
            .NotEmpty().WithMessage("Дата и время начала события не должны быть пустыми");
        RuleFor(c => c.EndsAtUtc)
            .Must((x, endsAt) => endsAt > x.StartsAtUtc)
            .When(x => x.EndsAtUtc.HasValue)
            .WithMessage("Дата и время окончания события должна быть больше или равна дате и времени начала события");

    }
}
