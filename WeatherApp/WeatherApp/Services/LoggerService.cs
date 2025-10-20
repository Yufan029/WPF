using NLog;
using WeatherApp.Interfaces;

namespace WeatherApp.Services
{
    /// <summary>
    /// Log service wrapper of the nlog.
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Log debug.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// Log error.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogError(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Log info.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Log warning.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
