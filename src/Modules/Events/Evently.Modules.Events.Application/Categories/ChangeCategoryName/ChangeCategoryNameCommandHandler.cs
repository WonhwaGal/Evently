using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Categories;

namespace Evently.Modules.Events.Application.Categories.ChangeCategoryName;
internal sealed class ChangeCategoryNameCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ChangeCategoryNameCommand>
{
    public async Task<Result> Handle(ChangeCategoryNameCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure(CategoryErrors.NotFound(request.CategoryId));
        }

        category.ChangeName(request.NewName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
