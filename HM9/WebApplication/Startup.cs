using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) =>
            services.AddControllersWithViews();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            (env.IsDevelopment()
                ? app.UseDeveloperExceptionPage()
                : app.UseExceptionHandler("/Home/Error").UseHsts()).UseHttpsRedirection().UseStaticFiles()
            .UseRouting()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"));
    }
}
