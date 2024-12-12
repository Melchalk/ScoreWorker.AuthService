using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuthService.Infrastructure.Middlewares;

public class TokenMiddleware(RequestDelegate next)
{
    private const string ErrorMessage = "Token validation is failed.";

    public async Task InvokeAsync(HttpContext context)
    {
        /*
        if (string.Equals(context.Request.Method, "OPTIONS", StringComparison.OrdinalIgnoreCase) ||
            context.Request.Path.StartsWithSegments(new PathString("/swagger")))
        {
            await next(context);
            return;
        }

        if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("/hangfire"))
        {
            await next(context);
            return;
        }

        var endpoint = context.GetEndpoint() ?? throw new UnauthorizedException(ErrorMessage);

        var isAllowAnonymous = endpoint.Metadata.OfType<AllowAnonymousAttribute>().Any();
        if (isAllowAnonymous)
        {
            await next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedException(ErrorMessage);
        }
        var claims = authService.ValidateToken(token);

        var login = claims.Claims.FirstOrDefault(x => x.Type == Claims.Login.ToString())?.Value;
        var tokenType = claims.Claims.FirstOrDefault(x => x.Type == Claims.TokenType.ToString())?.Value;

        if (string.IsNullOrEmpty(login) ||
            string.IsNullOrEmpty(tokenType) ||
            tokenType != TokenType.Auth.ToString())
        {
            throw new UnauthorizedException(ErrorMessage);
        }

        context.Items[HttpContextHelper.Login] = login;

        await next(context);*/
    }
}