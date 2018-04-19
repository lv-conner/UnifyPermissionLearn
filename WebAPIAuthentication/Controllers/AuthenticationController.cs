using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIAuthentication.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication/[action]")]
    public class AuthenticationController : Controller
    {
        public async Task<string> Login()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,"Tim"),
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email,"141412@163.com"),
                new Claim("Sex","Man")
            };
            //Authentication Type is necessary, if not it,IsAuthenticate will be false;!!!important
            var claimsIdentity = new ClaimsIdentity(claims, "Access_Key");
            var prop = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(1),
                IsPersistent = true
            };
            var user = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(user, prop);
            return "login";
        }
        public async Task<string> Logout()
        {
            await HttpContext.SignOutAsync();
            return "logout";
        }
    }
}