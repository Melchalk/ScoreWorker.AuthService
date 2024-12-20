using AuthService.Broker.Consumers;
using AuthService.Business;
using AuthService.Business.Interfaces;
using AuthService.Infrastructure.Middlewares;
using AuthService.Models.Dto.Configurations;
using AuthService.Token.Helpers;
using AuthService.Token.Helpers.Interfaces;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AuthService;

internal class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

        services.AddControllers();

        //ConfigureMassTransit(services);

        services.AddEndpointsApiExplorer();

        services.AddHttpContextAccessor();

        ConfigureJwt(services);

        ConfigureDI(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("CorsPolicy");

        app.UseHttpsRedirection();

        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseRouting();

        app.UseMiddleware<TokenMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    #region Private

    private void ConfigureMassTransit(IServiceCollection services)
    {
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost");
                cfg.ConfigureEndpoints(context);
            });

            ConfigureConsumers(busConfigurator);
        });
    }

    private void ConfigureConsumers(IServiceCollectionBusConfigurator x)
    {
        x.AddConsumer<CheckTokenConsumer>();

        x.AddConsumer<GetTokenConsumer>();
    }

    private void ConfigureDI(IServiceCollection services)
    {
        services.AddScoped<ILoginCommand, LoginCommand>();

        services.AddScoped<IRefreshTokenCommand, RefreshTokenCommand>();
    }

    private void ConfigureJwt(IServiceCollection services)
    {
        var signingKey = new SigningSymmetricKey();
        var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

        services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
        services.AddSingleton<IJwtSigningDecodingKey>(signingKey);

        services.AddTransient<ITokenGenerator, TokenGenerator>();
        services.AddTransient<ITokenValidator, TokenValidator>();

        services.Configure<TokenSettings>(Configuration.GetSection("TokenSettings"));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetSection("TokenSettings:TokenIssuer").Value,
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetSection("TokenSettings:TokenAudience").Value,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingDecodingKey.GetKey()
                };
            });

        services.AddAuthorization();
    }

    #endregion
}