namespace AuthService.Models.Dto.DTOs;

/// <summary>
/// Token configuration class filled in Startup.cs.
/// </summary>
public class TokenSettings
{
    public int AccessTokenLifetimeInMinutes { get; set; }
    public int RefreshTokenLifetimeInMinutes { get; set; }
    public required string TokenIssuer { get; set; }
    public required string TokenAudience { get; set; }
}