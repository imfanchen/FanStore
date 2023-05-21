namespace FanStore.Server.Authorization;

public static class ClaimTypes
{
    public const string JsonWebTokenId = "jti";
    public const string Principal = "sub";
    public const string Audience = "aud";
    public const string Role = "role";
    public const string Scope = "scope";
    public const string ActiveTime = "nbf";
    public const string InactiveTime = "exp";
    public const string CreatedTime = "iat";
    public const string Issuer = "iss";
}