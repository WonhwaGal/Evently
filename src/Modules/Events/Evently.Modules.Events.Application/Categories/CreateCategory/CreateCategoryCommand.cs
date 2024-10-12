
using Evently.Modules.Events.Application.Messaging;

namespace Evently.Modules.Events.Application.Categories.CreateCategory;
public sealed record CreateCategoryCommand(string CategoryName) : ICommand<Guid>;

