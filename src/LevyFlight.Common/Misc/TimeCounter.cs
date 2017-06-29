using System;

namespace LevyFlight.Common.Misc
{
    public class Timer
    {
        private static DateTime Now => DateTime.Now;

        private DateTime? StartTime { get; set; }
        private DateTime? EndTime { get; set; }

        public TimeSpan Elapsed => EndTime.Value - StartTime.Value;

        public void Start()
        {
            if (StartTime != null)
            {
                throw new InvalidOperationException("Timer has already started");
            }

            StartTime = Now;
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
        }
    }
}
