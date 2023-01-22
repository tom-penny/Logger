namespace Logger;

public enum LogType
{
    Xml,
    Text,
    Sql
}

public struct LoggerConfig
{
    public LogType Type { get; set; }
    public LogLevel Level { get; set; }
    public string Path { get; set; }
}