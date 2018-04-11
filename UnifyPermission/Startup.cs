using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UnifyPermission.Filter;
using UnifyPermission.Handler;
using UnifyPermission.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            //services.AddAuthentication(options=> 
            //{
            //    options.DefaultAuthenticateScheme = "Token";
            //    options.DefaultSignInScheme = "Token";
            //    options.AddScheme("Customer", builder =>
            //     {
            //         builder.HandlerType = typeof(CustomerAuthenticationHandler);
            //         builder.DisplayName = "Customer";
            //     });
            //}).AddCookie("Token",options =>
            //{
            //    options.LoginPath = "/Login/Login";
            //    options.ClaimsIssuer = "Token";
            //    options.AccessDeniedPath = "/Home/Index";
            //    options.Events.OnValidatePrincipal = context =>
            //    {
            //        return Task.CompletedTask;
            //    };
            //});

            //standard cookie authentication
            services.AddAuthentication("TokenAuth")
                .AddCookie("TokenAuth", options => 
                {
                    options.LoginPath = "/Login/Login";
                    //LogoutPath to check should redirect to 
                    options.LogoutPath = "/Login/Logout";
                });
            //Filter的注册方式，决定了Filter的提供方式的不同，如果使用Add方法进行注册，将会通过类型激活的方式进行提供
            //使用AddService的方式注册，将会通过依赖注入的方式进行提供。两者的提供方式中，构造函数均可以注入参数。
            //两种方法的不同之处在通过依赖注入方式提供的实例，可以提供其生命周期的管理，比如提供单实例
            services.AddMvc(options=>
            {
                //options中的Filters将作为全局过滤器。
                //options.Filters.AddService(typeof(LoggerFilter));
                options.Filters.Add(typeof(LoggerFilter));
            });
            services.AddSingleton<LoggerFilter,LoggerFilter>();
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
            //app.UseDirectoryBrowser();
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
