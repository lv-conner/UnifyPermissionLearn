using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnifyPermission.Attributes;
using UnifyPermission.Controllers.Base;
using UnifyPermission.Filter;
using UnifyPermission.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;

namespace UnifyPermission.Controllers
{
    [ActionPermission]
    [Filter(typeof(LoggerFilter))]  //通用特性过滤器实现。
    //[Logger]
    //[ModuleNo("001",ModuleName ="auth")]
    public class HomeController : AuthorizationController
    {
        private readonly IServiceCollection services;
        private readonly ILogger logger;
        public HomeController(IServiceCollection services,ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<HomeController>();
            this.services = services;
        }
        [ActionNo("Index")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AddCookie()
        {
            HttpContext.Response.Cookies.Append("CurrentTime", DateTime.UtcNow.ToString(), new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Expires = DateTime.Now.AddHours(2),
            });
            return Content("Add Cookie Successful");
        }

        public IActionResult UserInfo()
        {
            return Json(HttpContext.User.Identities);
        }

        public IActionResult GetServices()
        {

            return Json(services.Select(p=>new { InterfaceType=p.ServiceType.FullName,ServiceType=p.ImplementationType!=null?p.ImplementationType.FullName:p.ImplementationInstance.GetType().FullName }), new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
