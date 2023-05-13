using FanStore.Server.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapBooksEndpoints();

app.Run();