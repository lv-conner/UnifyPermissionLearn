using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UnifyPermission.Attributes;
using UnifyPermission.Controllers.Base;
using UnifyPermission.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnifyPermission.Controllers
{
    public class LoginController : AuthorizationController
    {
        private readonly IAuthenticationService service;
        public LoginController(IAuthenticationService service)
        {
            this.service = service;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Anonymous]
        public async Task<IActionResult> Login()
        {
            //var model = new UserModel() { Name = "tim", Password = "123456" };
            //HttpContext.Response.Cookies.Append("Token", JsonConvert.SerializeObject(model));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,"Tim"),
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email,"141412@163.com"),
                new Claim("Sex","Man")
            };
            //Authentication Type is necessary, if not it,IsAuthenticate will be false;!!!important
            var claimsIdentity = new ClaimsIdentity(claims,"Token");
            var prop = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(1)
            };
            var user = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(user);
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await service.SignOutAsync(HttpContext, "Token", null);
            return Redirect("/Login/Login");
        }
    }
}
