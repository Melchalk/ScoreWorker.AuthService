using AuthService.Infrastructure.Swagger;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace AuthService.Infrastructure.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
    {
        return builder.AddFilters().AddSwagger().AddSerilog();
    }

    private static IHostBuilder AddFilters(this IHostBuilder builder)
    {
        return builder.ConfigureServices(services =>
        {
            services.AddSingleton<IStartupFilter, TerminalStartupFilter>();
        });
    }

    private static IHostBuilder AddSwagger(this IHostBuilder builder)
    {
        return builder.ConfigureServices(services =>
        {
            services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"{Assembly.GetExecutingAssembly().GetName().Name}",
                    Version = "v1"
                });

                options.CustomSchemaIds(x => x.FullName);

                options.EnableAnnotations();

                //options.OperationFilter<HeaderOperationFilter>();
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        });
    }

    private static IHostBuilder AddSerilog(this IHostBuilder builder)
    {
        builder.ConfigureServices((hostContext, services) =>
        {
            Log.Logger = new LoggerConfiguration().ReadFrom
                .Configuration(hostContext.Configuration)
                .Enrich.WithProperty("Service", "Pro.Pricer.Service")
                .CreateLogger();

            services.AddSingleton(Log.Logger);
        });

        builder.UseSerilog();

        return builder;
    }
}