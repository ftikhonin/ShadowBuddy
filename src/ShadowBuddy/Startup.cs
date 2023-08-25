using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Infrastructure.Repositories;
using ShadowBuddy.Interceptors;
using ShadowBuddy.Services;

namespace ShadowBuddy;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient<IAccountProcessingRepository, AccountProcessingRepository>();
        services.AddOptions();
        services.AddControllers();
        services.AddGrpc(options => options.Interceptors.Add<GrpcResponseExceptionInterceptor>());
        services.AddGrpcReflection();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGrpcService<AccountProcessingGrpcService>();
        });
    }
}