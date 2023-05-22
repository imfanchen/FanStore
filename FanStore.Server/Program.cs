using FanStore.Server.Authorization;
using FanStore.Server.Data;
using FanStore.Server.Endpoints;
using FanStore.Server.ErrorHandling;
using FanStore.Server.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddClaimBasedAuthorization();

builder.Services.AddApiVersioning(options => {
    options.DefaultApiVersion = new(1.0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Logging.AddJsonConsole(options =>
{
    options.JsonWriterOptions = new()
    {
        Indented = true
    };
});

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(corsBuilder => {
        string allowedOrigin = builder.Configuration["AllowedOrigin"] ??
            throw new InvalidOperationException("Allowed Origin is not set.");
        corsBuilder.WithOrigins(allowedOrigin)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

WebApplication app = builder.Build();

app.UseExceptionHandler(configure => configure.ConfigureExceptionHandler());
app.UseMiddleware<RequestTimingMiddleware>();

await app.Services.InitializeDatabase();

app.UseHttpLogging();
app.MapBooksEndpoints();
app.UseCors();

app.Run();