using FanStore.Server.Endpoints;
using FanStore.Server.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IBooksRepository, InMemoryBooksRepository>();

builder.Configuration.GetConnectionString("FanStoreContext");

WebApplication app = builder.Build();

app.MapBooksEndpoints();

app.Run();