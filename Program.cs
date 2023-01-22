using Microsoft.Extensions.Configuration;

namespace Logger;

public class Program
{
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json").Build();

        var logging = config.GetSection(nameof(LoggerConfig));
        var xmlConfig = logging.Get<LoggerConfig>();
        var textConfig = new LoggerConfig()
        {
            Level = LogLevel.Debug,
            Path = "C:/Users/user/Downloads/logs.txt",
            Type = LogType.Text
        };

        var logger = Logger.GetInstance();
        logger.AddHandler(new LogHandler(xmlConfig, new LogContextFactory()));
        logger.AddHandler(new LogHandler(textConfig, new LogContextFactory()));

        logger.Debug("This is a log.");
        logger.Info("This is a log.");
        logger.Error("This is a log.");
    }
}