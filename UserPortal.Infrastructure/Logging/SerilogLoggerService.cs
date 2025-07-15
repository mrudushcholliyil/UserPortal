using Serilog;
using UserPortal.Application.Common.Interfaces;

namespace UserPortal.Infrastructure.Logging
{
    public class SerilogLoggerService<T> : ILoggerService<T>
    {
        private readonly ILogger _logger;

        public SerilogLoggerService()
        {
            _logger = Log.ForContext<T>();
        }
        public void LogError(Exception ex, string message)
        {
            _logger.Error(ex, message);
        }

        public void LogInfo(string message)
        {
            _logger.Information(message);
        }
    }
}
