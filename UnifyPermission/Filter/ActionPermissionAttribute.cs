using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using UnifyPermission.Attributes;
using Microsoft.Extensions.Options;
using UnifyPermission.Models;
using UnifyPermission.Controllers;
using Microsoft.AspNetCore.Mvc;
using UnifyPermission.Result;
using UnifyPermission.Controllers.Base;

namespace UnifyPermission.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ActionPermissionAttribute : Attribute, IAuthorizationFilter
    {
        private ILoggerFactory _logger;
        private ILogger Logger
        {
            get
            {
                return _logger.CreateLogger<ActionPermissionAttribute>();
            }
        }

        public ActionPermissionAttribute()
        {
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var options = context.HttpContext.RequestServices.GetService<IOptions<SystemOptions>>().Value;
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor == null)
            {
                return;
            }
            if (descriptor.ControllerTypeInfo.IsAssignableFrom(typeof(AnonymousController)))
            {
                return;
            }
            if (descriptor.MethodInfo.GetCustomAttribute<AnonymousAttribute>() != null)
            {
                return;
            }
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                OnUnAuthorization(context);
            }
            var actionNoAttribute = descriptor.MethodInfo.GetCustomAttribute<ActionNoAttribute>(true);
            var moduleNoAttribute = descriptor.ControllerTypeInfo.GetCustomAttribute<ModuleNoAttribute>(true);
            var model = new PermissionModel();
            model.SystemNo = options.SystemNo;
            model.SystemName = options.SystemName;
            model.ModuleNo = moduleNoAttribute?.ModuleNo ?? descriptor.ControllerName;
            model.ModuleName = moduleNoAttribute?.ModuleName ?? descriptor.ControllerName;
            model.ActionNo = actionNoAttribute?.ActionNo ?? descriptor.ActionName;
            model.ActionName = actionNoAttribute?.ActionName ?? descriptor.ActionName;
            _logger = context.HttpContext.RequestServices.GetService<ILoggerFactory>();
            Logger.LogInformation($"{"Action:\t" + model + "\t" + "Authorization Complete!"}");
        }

        public void OnUnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.Request.Headers["X-Requested-With"].FirstOrDefault() != null)
            {
                context.Result = new UnauthorizationJsonResult(new { code="401",message="No Authencation" });
            }
            else
            {
                context.Result = new CustomerUnauthorizaedResult("/Login/Login");
            }
        }
    }
}
