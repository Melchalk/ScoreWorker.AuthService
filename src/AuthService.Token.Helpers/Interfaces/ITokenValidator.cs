using AuthService.Models.Dto.Enum;
using System.Security.Claims;

namespace AuthService.Token.Helpers.Interfaces;

/// <summary>
/// Represents interface for user token validator.
/// </summary>
public interface ITokenValidator
{
    /// <summary>
    /// Validate user token.
    /// </summary>
    /// <param name="token">User token.</param>
    /// <param name="tokenType">Is access or refresh token</param>
    ClaimsPrincipal Validate(string token);
}
