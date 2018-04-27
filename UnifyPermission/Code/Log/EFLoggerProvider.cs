using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnifyPermission.Options;

namespace UnifyPermission.Code.Log
{
    public class EFLoggerProvider : ILoggerProvider
    {
        private EFLoggerOptions options;
        public EFLoggerProvider(EFLoggerOptions options)
        {
            this.options = options;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new EFLogger(options);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
