namespace Contracts
{
    public interface ILoggerManager
    {
        public void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
