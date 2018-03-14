using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnifyPermission.Result
{
    public class CustomerUnauthorizaedResult: UnauthorizedResult
    {
        private readonly string loginUrl;
        public CustomerUnauthorizaedResult(string loginUrl)
        {
            this.loginUrl = loginUrl;
        }
        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = 302;
            context.HttpContext.Response.Headers.Add("location", loginUrl);
        }
        public override Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = 302;
            context.HttpContext.Response.Headers.Add("location", loginUrl);
            return Task.CompletedTask;
        }
    }
}
