namespace Code.Runtime.Services.LogService
{
    public interface ILogService
    {
        void Log(string msg);
        void LogError(string msg);
        void LogWarning(string msg);
    }
}