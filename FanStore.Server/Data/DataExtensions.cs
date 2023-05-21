using FanStore.Server.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FanStore.Server.Data;

public static class DataExtensions
{
    public static async Task InitializeDatabase(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        FanStoreContext context = scope.ServiceProvider.GetRequiredService<FanStoreContext>();
        await context.Database.MigrateAsync();

        string category = "Database Initializer";
        ILogger logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(category);
        logger.LogInformation(5, "The database is ready.");

    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("FanStoreContext");
        services.AddScoped<IBooksRepository, EntityFrameworkBooksRepository>()
                .AddSqlServer<FanStoreContext>(connectionString);
        return services;
    }
}