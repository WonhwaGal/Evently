using Evently.Modules.Events.Application.Messaging;

namespace Evently.Modules.Events.Application.Categories.ChangeCategoryName;

public sealed record ChangeCategoryNameCommand(
    Guid CategoryId,
    string NewName) : ICommand;
