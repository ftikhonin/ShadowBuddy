using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Hosting;

namespace ShadowPal;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(
            webBuilder => { webBuilder.UseStartup<Startup>(); });

    private static bool IsEnvironment(string environment) =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == environment;
}