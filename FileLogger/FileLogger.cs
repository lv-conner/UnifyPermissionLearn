using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Abstractions.Internal;
using Microsoft.Extensions.Logging.Internal;
namespace FileLogger
{
    public class FileLogger : ILogger
    {
        private readonly Func<string, LogLevel, bool> filter;
        private readonly string catalogName;
        private FileLoggerOptions options;
        private string filePath;

        public FileLogger(string catalogName, Func<string, LogLevel, bool> filter,FileLoggerOptions options) 
        {
            if (options == null)
            {
                throw new ArgumentNullException("FileLoggerOptions");
            }
            this.catalogName = catalogName;
            this.filter = filter;
            this.options = options;
            EnsurePath();
            GenerateFilePath();
        }

        private void EnsurePath()
        {
            if(!Directory.Exists(options.Path))
            {
                Directory.CreateDirectory(options.Path);
            }
        }
        private void GenerateFilePath()
        {
            if (options == null)
            {
                throw new ArgumentNullException("FileLoggerOptions");
            }
            var fileName = catalogName + options.FileExtension;
            filePath = Path.Combine(options.Path + fileName);
        }


        public IDisposable BeginScope<TState>(TState state)
        {
            return NullScope.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            if(logLevel == LogLevel.None)
            {
                return false; 
            }
            return filter(catalogName, logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            File.AppendAllText(filePath, formatter(state, exception) + "\n" ,Encoding.UTF8);
        }
    }
}
