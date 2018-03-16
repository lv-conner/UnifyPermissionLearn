using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnifyPermission.Handler;
using UnifyPermission.Models;

namespace UnifyPermission
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
            services.Configure<SystemOptions>(Configuration.GetSection("SysetmConfig"));
            //services.AddAuthentication(options =>
            //{
            //    options.AddScheme<CustomerAuthenticationHandler>("Customer", "LfgGroup");
            //    options.DefaultScheme = "Customer";
            //    options.DefaultChallengeScheme = "Customer";
            //    options.DefaultForbidScheme = "Customer";
            //}).AddCookie("TokenCustomer",option=> {
            //});
            services.AddAuthentication(options=> 
            {
                options.DefaultAuthenticateScheme = "Token";
                options.DefaultSignInScheme = "Token";
            }).AddCookie("Token",options =>
            {
                options.LoginPath = "/Login/Login";
                options.ClaimsIssuer = "Token";
                options.AccessDeniedPath = "/Home/Index";
                options.Events.OnValidatePrincipal = context =>
                {
                    return Task.CompletedTask;
                };
            });
            services.AddMvc();
            services.AddSingleton(services);    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
