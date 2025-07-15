namespace UserPortal.Application.Common.Interfaces
{

    public interface ILoggerService<T>
    {
        void LogInfo(string message);
        void LogError(Exception ex, string message);
    }
}
