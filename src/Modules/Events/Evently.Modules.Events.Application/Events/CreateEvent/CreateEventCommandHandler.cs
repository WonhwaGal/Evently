using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.Categories;
using Evently.Common.Application.Clock;

namespace Evently.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(
    IEventRepository eventRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateEventCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (request.StartsAtUtc < dateTimeProvider.UtcNow)
        {
            return Result.Failure<Guid>(EventErrors.StartDateInPast);
        }

        Category? category = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound(request.CategoryId));
        }

        if (category.IsArchived)
        {
            return Result.Failure<Guid>(CategoryErrors.IsArchived(request.CategoryId));
        }

        Result<Event> result = Event.Create(
            request.CategoryId,
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc,
            request.EndsAtUtc);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        eventRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
