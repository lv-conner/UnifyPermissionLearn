using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnifyPermission.Filter
{
    public class LoggerFilter : IActionFilter
    {
        private readonly ILoggerFactory factory;
        private ILogger logger
        {
            get
            {
                return factory.CreateLogger<LoggerFilter>();
            }
        }
        public LoggerFilter(ILoggerFactory factory)
        {
            this.factory = factory;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation(context.ActionDescriptor.DisplayName + DateTime.Now.ToString());
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation(context.ActionDescriptor.DisplayName + DateTime.Now.ToString());
        }
    }
}
