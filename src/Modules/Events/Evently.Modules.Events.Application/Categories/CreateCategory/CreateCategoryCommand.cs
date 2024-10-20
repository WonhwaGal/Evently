
using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.Categories.CreateCategory;
public sealed record CreateCategoryCommand(string CategoryName) : ICommand<Guid>;

