using AuthService.Models.Dto.Configurations;
using AuthService.Models.Dto.Exceptions;
using AuthService.Token.Helpers.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthService.Token.Helpers;

public class TokenValidator(
    IOptions<TokenSettings> tokenSettings,
    IJwtSigningDecodingKey decodingKey) : ITokenValidator
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    public ClaimsPrincipal Validate(string token)
    {
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _tokenSettings.TokenIssuer,
                ValidateAudience = true,
                ValidAudience = _tokenSettings.TokenAudience,
                ValidateLifetime = true,
                IssuerSigningKey = decodingKey.GetKey(),
                ValidateIssuerSigningKey = true
            };

            return new JwtSecurityTokenHandler()
                .ValidateToken(token, validationParameters, out _);
        }
        catch (SecurityTokenValidationException)
        {
            throw new UnauthorizedException("Token failed validation.");
        }
        catch (Exception)
        {
            throw new BadRequestException("Token format was wrong.");
        }
    }
}
