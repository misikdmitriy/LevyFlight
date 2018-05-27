using NLog;
using ILogger = LevyFlight.Logging.Contracts.ILogger;

namespace LevyFlight.Logging
{
    public class Logger : ILogger
    {
        private readonly NLog.ILogger _logger;

        static Logger()
        {
            LogManager.LoadConfiguration("NLog.config");
        }

        public Logger(NLog.ILogger logger)
        {
            _logger = logger;
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }
    }
}
