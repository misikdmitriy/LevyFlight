namespace LevyFlight.Logging.Contracts
{
    public interface ILogger
    {
        void Trace(string message);
        void Debug(string message);
        void Info(string message);
        void Fatal(string message);
        void Error(string message);
    }
}
