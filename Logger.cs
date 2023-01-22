namespace Logger;

public enum LogLevel
{
    Debug,
    Info,
    Error
}

public interface ILogger
{
    void Debug(string message);
    void Info(string message);
    void Error(string message);
}

public class Logger : ILogger
{
    private static readonly Lazy<Logger> Instance = new(Initialise);

    private readonly List<ILogHandler> _handlers = new();

    private Logger() { }

    public static Logger GetInstance()
    {
        return Instance.Value;
    }

    public void Debug(string message)
    {
        foreach (var handler in _handlers)
        {
            handler.Log(LogLevel.Debug, message);
        }
    }

    public void Info(string message)
    {
        foreach (var handler in _handlers)
        {
            handler.Log(LogLevel.Info, message);
        }
    }

    public void Error(string message)
    {
        foreach (var handler in _handlers)
        {
            handler.Log(LogLevel.Error, message);
        }
    }

    public void AddHandler(ILogHandler handler)
    {
        _handlers.Add(handler);
    }

    public void RemoveHandler(ILogHandler handler)
    {
        _handlers.Remove(handler);
    }

    private static Logger Initialise()
    {
        return (Logger)Activator.CreateInstance(typeof(Logger), true)!;
    }
}