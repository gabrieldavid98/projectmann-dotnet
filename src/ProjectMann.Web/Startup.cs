using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectMann.Core.Domain;
using ProjectMann.Infrastructure.Extensions;
using ProjectMann.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProjectMann.Web.Managers;

namespace ProjectMann.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config => 
                {
                    config.ExpireTimeSpan = TimeSpan.FromHours(1);
                    config.LoginPath = "/Auth/Login";
                    config.LogoutPath = "/Auth/Logout";
                });

            services.AddInfrastructure(Configuration.GetConnectionString("ProjectMannConnection"));            

            services.AddScoped<IAuthManager, AuthManager>();

            services.AddAuthorization(options => 
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("1"));
                options.AddPolicy("AdminAndDev", policy => 
                    policy.RequireRole("1", "2")
                );
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
