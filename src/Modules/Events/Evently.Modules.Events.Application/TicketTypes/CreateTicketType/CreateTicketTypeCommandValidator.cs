using FluentValidation;

namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType;
internal sealed class CreateTicketTypeCommandValidator : AbstractValidator<CreateTicketTypeCommand>
{
    public CreateTicketTypeCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty().WithMessage("Идентификатор события не должен быть пустым");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Наименование типа билета не должно быть пустым")
            .MaximumLength(30).WithMessage("Наименование типа билета не должно иметь больше 30 символов");
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Описание события не должно быть пустым")
            .GreaterThan(0).WithMessage("Стоимость билета должна быть больше нуля")
            .PrecisionScale(6, 2, false).WithMessage("Стоимость билета не должна превышать 6 цифр в целом или иметь более 2 цифр после запятой");
        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Валюта стоимости билета не должна быть пустой");
        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Количество билетов для данного типа не должно быть пустым")
            .GreaterThan(0).WithMessage("Количество билетов должно быть больше нуля")
            .PrecisionScale(6, 0, false).WithMessage("Количество билетов должно быть целым числом и иметь не более 6 цифр");
    }
}
