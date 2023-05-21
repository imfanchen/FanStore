using FanStore.Server.Data;
using FanStore.Server.Endpoints;
using FanStore.Server.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IBooksRepository, InMemoryBooksRepository>();

string? connectionString = builder.Configuration.GetConnectionString("FanStoreContext");
builder.Services.AddSqlServer<FanStoreContext>(connectionString);

WebApplication app = builder.Build();

app.MapBooksEndpoints();

app.Run();