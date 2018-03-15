using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnifyPermission.Models;

namespace UnifyPermission.Handler
{
    //IAuthenticationRequestHandler
    public class CustomerAuthenticationHandler : IAuthenticationRequestHandler
    {
        private readonly HttpContext context;
        public CustomerAuthenticationHandler(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor.HttpContext;
        }
        public async Task<AuthenticateResult> AuthenticateAsync()
        {
            if(context.Request.Cookies["Token"] == null)
            {
                return AuthenticateResult.Fail("No Token");
            }
            var token = context.Request.Cookies["Token"];
            var model = JsonConvert.DeserializeObject<UserModel>(token);
            if(model == null)
            {
                return AuthenticateResult.Fail("No Token");
            }
            var identity = new GenericIdentity(model.Name);
            var claim = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(claim, "Lfg");
            var result = AuthenticateResult.Success(ticket);
            return await Task.FromResult(result);
        }

        public async Task ChallengeAsync(AuthenticationProperties properties)
        {
            await Task.CompletedTask;
        }

        public async Task ForbidAsync(AuthenticationProperties properties)
        {
            await Task.CompletedTask;
        }

        public async Task<bool> HandleRequestAsync()
        {
            return await Task.FromResult(false);
        }

        public async Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            await Task.CompletedTask;
        }
    }
}
