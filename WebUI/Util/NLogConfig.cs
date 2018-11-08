using NLog;

namespace WebUI.Util
{
    public class NLogConfig
    {
        public static void Configure()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = MyConfiguration.LOG_FILE,
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${logger} ${message} ${exception}"
            };

            config.AddRuleForAllLevels(logfile);

            NLog.LogManager.Configuration = config;
        }
    }
}