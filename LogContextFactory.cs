namespace Logger;

public class LogContextFactory
{
    public ILogContext Create(LogType logType, string path) => logType switch
    {
        LogType.Xml => new XmlLogContext(path),
        LogType.Text => new TextLogContext(path),
        LogType.Sql => new SqlLogContext(path),
        _ => throw new ArgumentException()
    };
}