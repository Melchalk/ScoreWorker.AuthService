using AuthService.Models.Dto.Exceptions;
using AuthService.Models.Dto.Responses;
using Serilog;
using System.Net;
using System.Text.Json;

namespace AuthService.Infrastructure.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            Log.Logger.Error("Exception was thrown {ex}", ex);

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is BaseException customException)
        {
            context.Response.StatusCode = (int)customException.StatusCode;
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        await context.Response.WriteAsync(JsonSerializer.Serialize(
            new ResponseInfo<object>()
            {
                ErrorMessage = exception.Message,
                Status = context.Response.StatusCode
            },
            options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
    }
}