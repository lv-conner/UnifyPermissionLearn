using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FileLogger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private static readonly Func<string, LogLevel, bool> trueFilter = (cat, level) => true;
        private readonly ConcurrentDictionary<string, FileLogger> loggers = new ConcurrentDictionary<string, FileLogger>();
        private readonly Func<string, LogLevel, bool> filter;
        private FileLoggerOptions options;
        private IDisposable _reloadChangeToken;

        public FileLoggerProvider(Func<string,LogLevel,bool> filter):this(filter,null)
        {
            
        }
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options):this(trueFilter,options)
        {

        }

        public FileLoggerProvider(Func<string,LogLevel,bool> filter, IOptionsMonitor<FileLoggerOptions> options)
        {
            if(options==null)
            {
                this.options = new FileLoggerOptions();
            }
            else
            {
                this.options = options.CurrentValue;
                _reloadChangeToken = options.OnChange(Reload);
            }
            this.filter = filter;
        }
        public void Reload(FileLoggerOptions options)
        {
            this.options = options;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, CreateLoggerImplement);
        }

        public FileLogger CreateLoggerImplement(string name)
        {
            return new FileLogger(name,filter,options);
        }

        public void Dispose()
        {
            _reloadChangeToken.Dispose();
        }
    }
}
