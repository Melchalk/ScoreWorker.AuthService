namespace AuthService.Models.Dto.Responses;

public record LoginResult
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
    public double AccessTokenExpiresIn { get; init; }
    public double RefreshTokenExpiresIn { get; init; }
}