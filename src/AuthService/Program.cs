using AuthService.Infrastructure.Extensions;
using Serilog;

namespace AuthService;

public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception exc)
        {
            Log.Fatal(exc, "Can not properly start Service.");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .AddInfrastructure()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}