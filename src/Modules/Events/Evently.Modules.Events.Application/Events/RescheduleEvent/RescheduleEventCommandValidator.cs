using FluentValidation;

namespace Evently.Modules.Events.Application.Events.RescheduleEvent;
internal sealed class RescheduleEventCommandValidator : AbstractValidator<RescheduleEventCommand> 
{
    public RescheduleEventCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty().WithMessage("Идентификатор мероприятия не должен быть пустым");
        RuleFor(x => x.EndsAtUtc)
            .Must((x, endsAt) => x.StartsAtUtc > endsAt)
            .When(x => x.EndsAtUtc.HasValue)
            .WithMessage("Дата и время окончания события должна быть больше дате и времени начала события");
    }
}
