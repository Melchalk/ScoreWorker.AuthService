using Microsoft.AspNetCore.Builder;

namespace AuthService.Infrastructure.Swagger;

public class SwaggerStartupFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            app.UseSwagger();
            app.UseSwagger();
            next(app);
        };
    }
}