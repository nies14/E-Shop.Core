using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Ocelot.ApiGateway;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((env, config) =>
            {                var environmentName = env.HostingEnvironment.EnvironmentName;
                Console.WriteLine($"Current environment: {environmentName}");
                // Then try the original Docker configuration
                config.AddJsonFile("ocelot.Docker.json", optional: true, reloadOnChange: true);
                // Then try environment-specific configuration
                config.AddJsonFile($"ocelot.{environmentName}.json", optional: true, reloadOnChange: true);
            }).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).UseSerilog(EShop.Logging.Logging.ConfigureLogger);
}