using System.Net;
using Microsoft.Extensions.Primitives;

namespace AuthService.Models.Dto.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(StringValues errors, HttpStatusCode statusCode) : base(errors)
    {
        Errors = errors;
        StatusCode = statusCode;
    }

    protected BaseException(StringValues errors, HttpStatusCode statusCode, Exception? innerException) : base(errors, innerException)
    {
        Errors = errors;
        StatusCode = statusCode;
    }

    public StringValues Errors { get; }
    public HttpStatusCode StatusCode { get; }
}