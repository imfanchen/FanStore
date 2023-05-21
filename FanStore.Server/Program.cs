using FanStore.Server.Data;
using FanStore.Server.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);

WebApplication app = builder.Build();

await app.Services.InitializeDatabase();

app.MapBooksEndpoints();

app.Run();