using System;

using LevyFlightSharp.Algorithms;
using LevyFlightSharp.Domain;
using LevyFlightSharp.Services;

using Microsoft.Extensions.Logging;

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
            _logger = ConfigurationService
                .LoggerFactory
                .CreateLogger(GetType().FullName);

            _logger.LogInformation("Tests started");
        }

        public virtual void CheckMethod(int steps, double expected, double eps)
        {
            var solutionFounded = false;

            _timer.Start();

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
            _logger.LogInformation("Tests finished");
        }
    }
}
