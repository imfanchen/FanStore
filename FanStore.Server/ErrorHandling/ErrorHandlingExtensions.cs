using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FanStore.Server.ErrorHandling;

public static class ErrorHandlingExtensions
{
    // Handle all the request exceptions without explicitly specifying try-catch-block in endpoints
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            ILogger logger = context.RequestServices
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger("Error Handling");
            IExceptionHandlerFeature? exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
            Exception? exception = exceptionDetails?.Error;
            logger.LogError(
                exception,
                "Could not process a request on machine {Machine}. TraceId: {TraceId}",
                Environment.MachineName,
                Activity.Current?.TraceId
            );
            var problem = new ProblemDetails
            {
                Title = "We made a mistake but we're working on it!",
                Status = StatusCodes.Status500InternalServerError,
                Extensions =
                {
                    {"traceId", Activity.Current?.TraceId.ToString()}
                }
            };
            var environment = context.RequestServices.GetRequiredService<IHostEnvironment>();
            if (environment.IsDevelopment())
            {
                problem.Detail = exception?.ToString();
            }
            await Results.Problem(problem).ExecuteAsync(context);
        });
    }
}