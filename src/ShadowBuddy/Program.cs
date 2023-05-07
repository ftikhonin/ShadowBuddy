using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices.ComTypes;

namespace ShadowBuddy;

public class Program
{
    public static void Main(string[] args)
    {
        ConfigureBuilder(args).Build().Run();
    }

    public static IHostBuilder ConfigureBuilder(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);
        return builder;
    }

    private static bool IsEnvironment(string environment) =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == environment;
}