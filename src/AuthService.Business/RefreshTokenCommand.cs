using AuthService.Business.Interfaces;
using AuthService.Models.Dto.Enum;
using AuthService.Models.Dto.Exceptions;
using AuthService.Models.Dto.Requests;
using AuthService.Models.Dto.Responses;
using AuthService.Token.Helpers.Interfaces;

namespace AuthService.Business;

public class RefreshTokenCommand(
    ITokenValidator tokenValidator,
    ITokenGenerator tokenGenerator) : IRefreshTokenCommand
{
    public LoginResult Execute(RefreshRequest request)
    {
        var claims = tokenValidator.Validate(request.RefreshToken);
        var tokenType = claims.Claims.FirstOrDefault(x => x.Type == Claims.TokenType.ToString())?.Value;

        var parseResult = Guid.TryParse(
            claims.Claims.FirstOrDefault(x => x.Type == Claims.UserId.ToString())?.Value,
            out Guid userId);

        if (!parseResult || tokenType != TokenType.Refresh.ToString())
        {
            throw new UnauthorizedException("Token validation is failed.");
        }

        return new LoginResult
        {
            AccessToken = tokenGenerator.GenerateToken(userId, TokenType.Access, out DateTime accessTokenLifetime),
            RefreshToken = tokenGenerator.GenerateToken(userId, TokenType.Refresh, out DateTime refreshTokenLifetime),
            AccessTokenExpiresIn = accessTokenLifetime.ToOADate(),
            RefreshTokenExpiresIn = refreshTokenLifetime.ToOADate()
        };
    }
}
