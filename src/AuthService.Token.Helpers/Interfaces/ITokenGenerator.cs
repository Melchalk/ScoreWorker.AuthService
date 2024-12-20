using AuthService.Models.Dto.Enum;

namespace AuthService.Token.Helpers.Interfaces;

public interface ITokenGenerator
{
    /// <summary>
    /// Create new refresh or access token based on user id.
    /// </summary>
    /// <param name="userId">Specified user ID</param>
    /// <param name="tokenType">Token type (Access, Refresh)</param>
    /// <returns>Token based on userId</returns>
    string GenerateToken(Guid userId, TokenType tokenType, out DateTime tokenLifetime);
}
