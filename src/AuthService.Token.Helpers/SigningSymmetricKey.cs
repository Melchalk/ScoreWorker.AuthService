using System.Text;
using AuthService.Token.Helpers.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Token.Helpers;

public class SigningSymmetricKey : IJwtSigningEncodingKey, IJwtSigningDecodingKey
{
    private const string SigningSecurityKey = "H7rxZXtKVjFPBsLMUUOxp74iDnPbdaFOjrZhSOFHHbgfobp7y0M2lswkqs5NiTAeZO";

    private readonly SymmetricSecurityKey _secretKey = new(Encoding.UTF8.GetBytes(SigningSecurityKey));

    public string SigningAlgorithm => SecurityAlgorithms.HmacSha512;

    public SecurityKey GetKey() => _secretKey;
}