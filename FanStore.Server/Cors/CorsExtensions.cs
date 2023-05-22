namespace FanStore.Server.Cors;

public static class CorsExtensions
{
    private const string allowedOriginSetting = "AllowedOrigin";
    public static IServiceCollection AddCorsService(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddCors(options =>
        {
            options.AddDefaultPolicy(corsBuilder =>
            {
                string allowedOrigin = configuration[allowedOriginSetting] ??
                    throw new InvalidOperationException("Allowed Origin is not set.");
                corsBuilder.WithOrigins(allowedOrigin)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}