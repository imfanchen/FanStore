using FanStore.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace FanStore.Server.Data;

public static class DataExtensions {
    public static void InitializeDatabase(this IServiceProvider serviceProvider) {
        using IServiceScope scope = serviceProvider.CreateScope();
        FanStoreContext context = scope.ServiceProvider.GetRequiredService<FanStoreContext>();
        context.Database.Migrate();
    }
}