using FanStore.Server.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FanStore.Server.Data;

public static class DataExtensions
{
    public static void InitializeDatabase(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        FanStoreContext context = scope.ServiceProvider.GetRequiredService<FanStoreContext>();
        context.Database.Migrate();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("FanStoreContext");
        services.AddScoped<IBooksRepository, EntityFrameworkBooksRepository>()
                .AddSqlServer<FanStoreContext>(connectionString);
        return services;
    }
}