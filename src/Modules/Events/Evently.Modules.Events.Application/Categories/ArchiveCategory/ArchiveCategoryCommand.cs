using Evently.Modules.Events.Application.Messaging;

namespace Evently.Modules.Events.Application.Categories.ArchiveCategory;
public sealed record ArchiveCategoryCommand(Guid Id) : ICommand;
