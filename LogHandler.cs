namespace Logger;

public interface ILogHandler
{
    void Log(LogLevel level, string message);
}

public class LogHandler : ILogHandler
{
    private readonly LoggerConfig _config;
    private readonly LogContextFactory _factory;

    public LogHandler(LoggerConfig config, LogContextFactory factory)
    {
        _config = config;
        _factory = factory;
    }

    public void Log(LogLevel level, string message)
    {
        var scope = new LogTransaction(GetContext());
        scope.LogRepository.Insert($"{DateTime.Now:u} {level}: {message}");
        scope.Complete();
    }

    private ILogContext GetContext()
    {
        return _factory.Create(_config.Type, _config.Path);
    }
}