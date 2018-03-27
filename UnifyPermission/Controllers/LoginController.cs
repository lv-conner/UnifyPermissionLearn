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
           
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,"Tim"),
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email,"141412@163.com"),
                new Claim("Sex","Man")
            };
            //Authentication Type is necessary, if not it,IsAuthenticate will be false;!!!important
            var claimsIdentity = new ClaimsIdentity(claims, "TokenAuth");
            var prop = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(1),
                IsPersistent = true
            };
            var user = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(user, prop);
            //var property = new AuthenticationProperties()
            //{
            //    AllowRefresh = true,
            //    ExpiresUtc = DateTime.UtcNow.AddHours(2),
            //    IsPersistent = true,
            //};
            //ClaimsIdentity identity = new ClaimsIdentity("TokenAuth");
            //identity.AddClaim(new Claim(ClaimTypes.Email, "234123@qq.com"));
            //identity.AddClaim(new Claim(ClaimTypes.Name, "Tim"));
            //ClaimsPrincipal user = new ClaimsPrincipal(identity);
            //await HttpContext.SignInAsync(user, property);

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            var authenticationFeature =  HttpContext.Features.Get<IAuthenticationFeature>();
            var prop = new AuthenticationProperties()
            {
                RedirectUri = "/Login/AfterLogout"
            };
            await HttpContext.SignOutAsync(prop);
            return View();
        }

        [Anonymous]
        public IActionResult AfterLogout()
        {
            return View();
        }
    }
}
