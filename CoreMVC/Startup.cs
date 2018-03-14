using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Middleware;
using CoreMVC.Options;
using CoreMVC.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CoreMVC
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
            services.AddOptions();
            services.Configure<WeChatOption>(p =>
            {
                p.WeChatID = "dsfasdfas";
                p.WeChatName = "dsfasd";
            });
            services.AddSingleton<IGetName, GetName>();
            services.AddMvc();
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
            app.UseTestMiddleware();
            app.UseMvcWithDefaultRoute();
        }
    }
}
