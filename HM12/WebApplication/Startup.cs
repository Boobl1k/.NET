using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication.Controllers;
using WebApplication.DB;
using WebApplication.Models;

namespace WebApplication;

public class Startup
{
    public Startup(IConfiguration configuration) =>
        Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMiniProfiler();
        
        services.AddScoped<IDbContext<ComputedExpression>, ComputedExpressionsContext>();
        services.AddScoped<ExpressionsCache>();
        services.AddSingleton<ICalculator, Calculator>();
        services.AddSingleton<ICachedCalculator, CachedCalculator>();
        services.AddControllersWithViews();
        services.AddSingleton<ExceptionHandler>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
        (env.IsDevelopment()
            ? app.UseDeveloperExceptionPage()
            : app.UseExceptionHandler("/Home/Error").UseHsts()).UseHttpsRedirection().UseStaticFiles()
        .UseRouting()
        .UseAuthorization()
        .UseMiniProfiler()
        .UseEndpoints(endpoints =>
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"));
}
