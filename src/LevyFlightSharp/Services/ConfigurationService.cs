using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;

using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace LevyFlightSharp.Services
{
    public static class ConfigurationService
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static ILoggerFactory LoggerFactory { get; private set; }
        private static bool _isDebug;

        static ConfigurationService()
        {
            ReconfigureApp();
            SetupDebug();
            ConfigureLogger();
        }

        public static void ReconfigureApp()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        private static void ConfigureLogger()
        {
            LoggerFactory = new LoggerFactory()
                .AddConsole(LogLevel.Information)
                .AddNLog();

            var logConfig = CreateLogConfiguration();

            LoggerFactory.ConfigureNLog(logConfig);
        }


        private static LoggingConfiguration CreateLogConfiguration()
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
        private static void SetupDebug()
        {
            _isDebug = true;
        }
    }
}
