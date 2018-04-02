using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnifyPermission.Options
{
    public class EFLoggerOptions
    {
        public EFLoggerOptions()
        {

        }
        public string ConnectionString { get; set; }
        public LogLevel logLevel { get; set; }
    }
}
