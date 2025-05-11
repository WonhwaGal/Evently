

namespace Evently.Common.Infrastructure.EventBus;
public sealed record RabbitMqSettings(
    string Host, 
    string userName = "guest", 
    string Password = "guest");

