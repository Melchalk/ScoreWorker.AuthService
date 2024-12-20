using AuthService.Models.Dto.Configurations;
using AuthService.Models.Dto.Enum;
using AuthService.Token.Helpers.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthService.Token.Helpers;

public class TokenGenerator(
    IOptions<TokenSettings> tokenSettings,
    IJwtSigningEncodingKey encodingKey) : ITokenGenerator
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    public string GenerateToken(
        Guid userId, TokenType tokenType, out DateTime tokenLifetime)
    {
        List<Claim> claims =
        [
            new(Claims.UserId.ToString(), userId.ToString()),
            new(Claims.TokenType.ToString(), tokenType.ToString())
        ];

        tokenLifetime = DateTime.UtcNow.Add(
            tokenType == TokenType.Access
                ? TimeSpan.FromMinutes(_tokenSettings.AccessTokenLifetimeInMinutes)
                : TimeSpan.FromMinutes(_tokenSettings.RefreshTokenLifetimeInMinutes));

        var jwt = new JwtSecurityToken(
            issuer: _tokenSettings.TokenIssuer,
            audience: _tokenSettings.TokenAudience,
            claims: claims,
            expires: tokenLifetime,
            signingCredentials: new SigningCredentials(
                encodingKey.GetKey(),
                encodingKey.SigningAlgorithm));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
