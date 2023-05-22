using FanStore.Server.Authorization;
using FanStore.Server.Data;
using FanStore.Server.Endpoints;
using FanStore.Server.Middleware;

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

app.UseMiddleware<RequestTimingMiddleware>();

await app.Services.InitializeDatabase();

app.UseHttpLogging();
app.MapBooksEndpoints();

app.Run();