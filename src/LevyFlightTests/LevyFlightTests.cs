using System;

using LevyFlightSharp.Algorithms;
using LevyFlightSharp.Domain;
using LevyFlightSharp.Services;

using Microsoft.Extensions.Logging;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using Xunit;

namespace LevyFlightTests
{
    public abstract class LevyFlightTests : IDisposable
    {
        private readonly LevyFlightAlgorithm _algorithm;
        private readonly TimeCounter _timer;
        private readonly ILogger _logger;

        protected LevyFlightTests(Func<double[], double> testFunction)
        {
            _algorithm = new LevyFlightAlgorithm(testFunction);
            _timer = new TimeCounter();

            var loggerFactory = new LoggerFactory()
                .AddConsole(LogLevel.Information)
                .AddNLog();

            var logConfig = CreateLogConfiguration();

            loggerFactory.ConfigureNLog(logConfig);

            _logger = loggerFactory.CreateLogger(GetType().FullName);
        }

        public virtual void CheckMethod(int steps, double expected, double eps)
        {
            var solutionFounded = false;

            _logger.LogInformation("Test information:");
            _logger.LogInformation("Class name - " + GetType().FullName);
            _logger.LogInformation("Eps - " + eps);
            _logger.LogInformation("Max step number - " + steps);

            _timer.Start();

            steps = steps == 0 ? int.MaxValue : steps;

            for (var i = 0; i < steps; i++)
            {
                var result = _algorithm.Polinate();

                _logger.LogInformation("Step " + i + ": function = " + result.CountFunction(Solution.Current));
                _logger.LogInformation("Values = " + result.ToString(Solution.Current));

                if (Math.Abs(result.CountFunction(Solution.Current) - expected) <= eps)
                {
                    _logger.LogInformation("Solution founded");
                    solutionFounded = true;
                    break;
                }
            }

            _timer.End();

            if (!solutionFounded)
            {
                Assert.True(false, "Solution wasn't founded");
            }

            var stepLength = _timer.Elapsed.Ticks / steps;

            _logger.LogInformation("Step length = " + TimeSpan.FromTicks(stepLength).Milliseconds);
        }

        public void Dispose()
        {
        }

        private LoggingConfiguration CreateLogConfiguration()
        {
            var logConfig = new LoggingConfiguration();

            // config target
            var fileTarget = new FileTarget
            {
                Name = "logFile",
                FileName = "${basedir}/logs/" + GetType().Name + "-" + Guid.NewGuid() + ".log",
                DeleteOldFileOnStartup = true
            };

            // config rule

            var rule = new LoggingRule("*", NLog.LogLevel.Info, fileTarget);

            logConfig.AddTarget(fileTarget);
            logConfig.LoggingRules.Add(rule);
            return logConfig;
        }
    }
}
