using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using System;

namespace LevyFlightSharp
{
    public class ConfigurationService
    {
        public static IConfigurationRoot Configuration { get; private set; }

        static ConfigurationService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public ILoggerFactory LoggerFactory { get; private set; }

        public ConfigurationService()
        {
            SetupDebug();
            ConfigureLogger();
        }

        private void ConfigureLogger()
        {
            LoggerFactory = new LoggerFactory()
                .AddConsole(LogLevel.Information)
                .AddNLog();

            var logConfig = CreateLogConfiguration();

            LoggerFactory.ConfigureNLog(logConfig);
        }

        private bool _isDebug;

        private LoggingConfiguration CreateLogConfiguration()
        {
            var logConfig = new LoggingConfiguration();

            // config target
            var fileTarget = new FileTarget
            {
                Name = "logFile",
                FileName = "${basedir}/logs/" + Guid.NewGuid() + ".log",
                DeleteOldFileOnStartup = true
            };

            // config rule

            var rule = _isDebug
                ? new LoggingRule("*", NLog.LogLevel.Debug, fileTarget) 
                : new LoggingRule("*", NLog.LogLevel.Info, fileTarget);

            logConfig.AddTarget(fileTarget);
            logConfig.LoggingRules.Add(rule);
            return logConfig;
        }

        [Conditional("DEBUG")]
        private void SetupDebug()
        {
            _isDebug = true;
        }
    }
}
