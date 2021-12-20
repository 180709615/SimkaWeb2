using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using APIConsume.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace APIConsume
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddMvc();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;


            });
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<SIATMAX_SISTERContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());
            services.AddDbContext<DATA_SISTERContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DataSisterConnection")).EnableSensitiveDataLogging());
            services.AddDbContext<DATA_SISTERContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SIATMA_UAJYConnection")).EnableSensitiveDataLogging());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddProgressiveWebApp();
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints => {

                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

                //nampaknya yang dibawah ini tidak berfungsi
                endpoints.MapControllerRoute(
                    name: "SemuaDosen",
                    pattern: "{controller=SemuaDosenController}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                  name: "SimkaHome",
                  pattern: "{controller=SimkaHomeController}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
               name: "SimkaPengembangan",
               pattern: "{controller=SimkaPengembanganController}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            app.UseCookiePolicy();

        }
    }


}