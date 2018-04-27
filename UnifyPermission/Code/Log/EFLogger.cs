using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnifyPermission.Options;

namespace UnifyPermission.Code.Log
{
    public class EFLogger : ILogger
    {
        private EFLoggerOptions options;
        public EFLogger(EFLoggerOptions options)
        {
            this.options = options;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            if(logLevel > options.logLevel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);

            throw new NotImplementedException();
        }
    }
}
