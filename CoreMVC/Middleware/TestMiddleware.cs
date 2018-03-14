using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CoreMVC.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IGetName _getName;

        public TestMiddleware(RequestDelegate next,IGetName getName)
        {
            _getName = getName;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync(_getName.Name);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TestMiddlewareExtensions
    {
        public static IApplicationBuilder UseTestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TestMiddleware>();
        }
    }
}
