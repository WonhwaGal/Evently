using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Categories;

namespace Evently.Modules.Events.Application.Categories.ArchiveCategory;
internal sealed class ArchiveCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ArchiveCategoryCommand>
{
    public async Task<Result> Handle(ArchiveCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if(category is null)
        {
            return Result.Failure(CategoryErrors.NotFound(request.Id));
        }

        category.Archive();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
