using FanStore.Server.Authorization;
using FanStore.Server.Data;
using FanStore.Server.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddClaimBasedAuthorization();

WebApplication app = builder.Build();

await app.Services.InitializeDatabase();

app.MapBooksEndpoints();

app.Run();