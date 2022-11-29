using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ServiceLayer.Tests
{
    public class LoggerTest
    {
        private readonly ILogger _logger;
        public LoggerTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging().BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

//using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
  //  ILogger logger = loggerFactory.CreateLogger<Program>();



                using var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter("Microsoft", LogLevel.Warning)
                        .AddFilter("System", LogLevel.Warning)
                        .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug);
                        //.AddConsole();
                });
                ILogger logger = loggerFactory.CreateLogger<LoggerTest>();
                logger.LogInformation("Example log message");

            _logger = factory?.CreateLogger("Info");
        }
        [Fact]
        public void FireLog()
        {
            _logger.LogInformation($"464646464");
            _logger.LogError("my deliberate error");
            _logger.LogDebug("just debug");
            Console.WriteLine("sdf");
            Debug.WriteLine("A");
        }
    }
}