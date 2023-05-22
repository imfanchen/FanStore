using System.Diagnostics;

namespace FanStore.Server.Middleware;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<RequestTimingMiddleware> logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    // HttpContext is passed in via Dependency Injection
    public async Task InvokeAsync(HttpContext context)
    {
        Stopwatch stopWatch = new();
        try
        {
            stopWatch.Start();
            await next(context);
        }
        finally
        {
            stopWatch.Stop();
            long elapsedMilliseconds = stopWatch.ElapsedMilliseconds;
            logger.LogInformation(
                "{RequestMethod} {RequestPath} request took {ElapsedMilliseconds}ms to complete",
                context.Request.Method,
                context.Request.Path,
                elapsedMilliseconds);
        }
    }
}