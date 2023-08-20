using System.Reflection;
using Calzolari.Grpc.AspNetCore.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShadowPal;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
    }
}