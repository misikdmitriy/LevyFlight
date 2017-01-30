using System;
using Microsoft.Extensions.Logging;

namespace LevyFlightSharp.Services
{
    public class TimeCounter
    {
        private static DateTime Now => DateTime.Now;

        private readonly ILogger _logger;

        private DateTime? StartTime { get; set; }
        private DateTime? EndTime { get; set; }

        public TimeSpan Elapsed => EndTime.Value - StartTime.Value;

        public TimeCounter()
        {
            _logger = ConfigurationService
                .LoggerFactory
                .CreateLogger(GetType().FullName);
        }

        public void Start()
        {
            if (StartTime != null)
            {
                throw new InvalidOperationException("Timer has already started");
            }

            StartTime = Now;
            _logger.LogInformation($"Event started at {StartTime.Value:T}");
        }

        public void End()
        {
            if (StartTime == null)
            {
                throw new InvalidOperationException("Timer hasn't started yet");
            }
            if (EndTime != null)
            {
                throw new InvalidOperationException("Timer has already finished");
            }

            EndTime = Now;

            _logger.LogInformation($"Event finished at {EndTime.Value:T}");
            _logger.LogInformation($"Time elapsed {Elapsed:T}");
        }
    }
}
