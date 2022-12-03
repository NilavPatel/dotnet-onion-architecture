using MyApp.Application.Core.Services;
using NLog.Web;

namespace MyApp.Infrastructure.Services
{
    public class LoggerService : ILoggerService
    {
        // Create nlog.config (lowercase all) file in the root of your project
        public NLog.Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public void LogError(string errorMessage)
        {
            logger.Error(errorMessage);
        }

        public void LogError(string errorMessage, params object[] args)
        {
            logger.Error(errorMessage, args);
        }

        public void LogException(Exception ex)
        {
            logger.Error(ex);
        }

        public void LogInfo(string infoMessage)
        {
            logger.Info(infoMessage);
        }

        public void LogInfo(string infoMessage, params object[] args)
        {
            logger.Info(infoMessage, args);
        }
    }
}
