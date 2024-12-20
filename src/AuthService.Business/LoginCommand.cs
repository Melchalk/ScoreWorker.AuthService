using AuthService.Business.Interfaces;
using AuthService.Models.Dto.Requests;
using AuthService.Models.Dto.Responses;

namespace AuthService.Business;

public class LoginCommand : ILoginCommand
{
    public Task<LoginResult> ExecuteAsync(LoginRequest request)
    {
        throw new NotImplementedException();
    }
}
