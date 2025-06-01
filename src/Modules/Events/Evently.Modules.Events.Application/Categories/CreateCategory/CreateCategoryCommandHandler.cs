using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Categories;

namespace Evently.Modules.Events.Application.Categories.CreateCategory;
internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? existingCategory = await categoryRepository.GetByNameAsync(request.CategoryName, cancellationToken);

        if (existingCategory is not null)
        {
            return Result.Failure<Guid>(CategoryErrors.AlreadyExists(request.CategoryName));
        }

        Result<Category> categoryResult = Category.Create(request.CategoryName);
        if (categoryResult.IsFailure)
        {
            return Result.Failure<Guid>(categoryResult.Error);
        }

        categoryRepository.Insert(categoryResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return categoryResult.Value.Id;
    }
}
