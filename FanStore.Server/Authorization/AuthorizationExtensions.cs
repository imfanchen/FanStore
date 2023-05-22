namespace FanStore.Server.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.ReadAccess, builder => builder
                .RequireClaim(ClaimTypes.Scope, Permissions.ReadBooks));
            options.AddPolicy(Policies.WriteAccess, builder => builder
                .RequireRole(Roles.Admin)
                .RequireClaim(ClaimTypes.Scope, Permissions.WriteBooks));
        });
        return services;
    }
}