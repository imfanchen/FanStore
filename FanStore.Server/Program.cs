using System.Diagnostics;
using FanStore.Server.Authorization;
using FanStore.Server.Data;
using FanStore.Server.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddClaimBasedAuthorization();

builder.Logging.AddJsonConsole(options =>
{
    options.JsonWriterOptions = new()
    {
        Indented = true
    };
});

WebApplication app = builder.Build();

app.Use(async (context, next) => 
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
        app.Logger.LogInformation(
            "{RequestMethod} {RequestPath} request took {ElapsedMilliseconds}ms to complete",
            context.Request.Method,
            context.Request.Path,
            elapsedMilliseconds);
    }
});

await app.Services.InitializeDatabase();

app.UseHttpLogging();
app.MapBooksEndpoints();

app.Run();