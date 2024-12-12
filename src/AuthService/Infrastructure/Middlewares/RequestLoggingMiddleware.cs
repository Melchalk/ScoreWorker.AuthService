namespace AuthService.Infrastructure.Middlewares;

public class RequestLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        LogRequest(context);

        await next(context);

        LogResponse(context);
    }

    private void LogRequest(HttpContext context)
    {
        if (string.Equals(context.Response.ContentType, "application/grpc",
                StringComparison.CurrentCultureIgnoreCase))
            return;

        try
        {
            context.Request.EnableBuffering();

            string fullRequestPath = context.Request.PathBase + context.Request.Path;

            logger.LogDebug("Request logged");
            logger.LogDebug("Headers: {headers}", context.Request.Headers);
            logger.LogDebug("Route: {route}", fullRequestPath);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not log request body");
        }
    }

    private void LogResponse(HttpContext context)
    {
        if (string.Equals(context.Response.ContentType, "application/grpc", StringComparison.CurrentCultureIgnoreCase))
            return;

        try
        {
            logger.LogDebug("Response logged");
            logger.LogDebug("Headers: {headers}", context.Response.Headers);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not log response body");
        }
    }
}