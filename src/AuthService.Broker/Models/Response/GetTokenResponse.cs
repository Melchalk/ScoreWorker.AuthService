namespace AuthService.Broker.Models.Response;

public class GetTokenResponse
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
    public double AccessTokenExpiresIn { get; init; }
    public double RefreshTokenExpiresIn { get; init; }
}
