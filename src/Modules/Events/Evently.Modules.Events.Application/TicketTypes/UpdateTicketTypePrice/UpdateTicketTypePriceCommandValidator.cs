using FluentValidation;

namespace Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
internal sealed class UpdateTicketTypePriceCommandValidator : 
    AbstractValidator<UpdateTicketTypePriceCommand>
{
    public UpdateTicketTypePriceCommandValidator()
    {
        RuleFor(x => x.TicketId)
            .NotEmpty().WithMessage("Идентификатор билета не должен быть пустым");
        RuleFor(x => x.NewPrice)
            .NotEmpty().WithMessage("Описание события не должно быть пустым")
            .GreaterThan(0).WithMessage("Стоимость билета должна быть больше нуля")
            .PrecisionScale(6, 2, false).WithMessage("Стоимость билета не должна превышать 6 цифр в целом или иметь более 2 цифр после запятой");
    }
}
