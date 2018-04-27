using FileLogger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices.Internal;
using System;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileTest.TestLog();
            Console.WriteLine("Hello World!");
        }
    }

    class FileTest
    {
        public static void TestLog()
        {
            IServiceProvider serviceProvider = new ServiceCollection()
            .AddOptions()
            .AddLogging()
            .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            factory.AddProvider(new FileLoggerProvider((s, l) => true));
            var logger = factory.CreateLogger("program");
            logger.LogInformation("hello");
            var logger2 = factory.CreateLogger("program");
            Debug.Assert(object.ReferenceEquals(logger, logger2));
            logger2.LogInformation("word");
            var logger3 = factory.CreateLogger("program1");
        }
    }
}
