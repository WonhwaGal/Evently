namespace Evently.Gateway.Middleware;

public static class MiddlewareExtensions
{
    internal static IApplicationBuilder UseLogContextTraceLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<LogContextTraceLoggingMiddleware>();

        return app;
    }
}
