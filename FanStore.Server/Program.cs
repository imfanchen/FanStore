using FanStore.Server.Data;
using FanStore.Server.Endpoints;
using FanStore.Server.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBooksRepository, EntityFrameworkBooksRepository>();
string? connectionString = builder.Configuration.GetConnectionString("FanStoreContext");
builder.Services.AddSqlServer<FanStoreContext>(connectionString);

WebApplication app = builder.Build();

app.Services.InitializeDatabase();

app.MapBooksEndpoints();

app.Run();