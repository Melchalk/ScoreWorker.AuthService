using AuthService.Models.Dto.Requests;
using AuthService.Models.Dto.Responses;

namespace AuthService.Business.Interfaces;

public interface IRefreshTokenCommand
{
    LoginResult Execute(RefreshRequest request);
}
