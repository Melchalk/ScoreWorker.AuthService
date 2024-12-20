using System.Net;
using Microsoft.Extensions.Primitives;

namespace AuthService.Models.Dto.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(StringValues message) : base(message, StatusCode)
    {
    }

    public BadRequestException(StringValues message, Exception? innerException) : base(message, StatusCode, innerException)
    {
    }

    private new const HttpStatusCode StatusCode = HttpStatusCode.BadRequest;
}