using System.Xml;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Logger;

public interface ILogContext : IDisposable
{
    IList<string> Set();
    void SaveChanges();
}

public class TextLogContext : ILogContext
{
    private readonly List<string> _logs = new();
    private readonly string _filePath;

    public TextLogContext(string filePath)
    {
        _filePath = filePath;
    }

    public void Dispose()
    {
        _logs.Clear();
    }

    public void SaveChanges()
    {
        using var writer = File.AppendText(_filePath);
        foreach (var log in _logs)
        {
            writer.WriteLine(log);
        }
    }

    public IList<string> Set()
    {
        return _logs;
    }
}

public class XmlLogContext : ILogContext
{
    private readonly List<string> _logs = new();
    private readonly string _filePath;

    public XmlLogContext(string filePath)
    {
        _filePath = filePath;
    }

    public void Dispose()
    {
        _logs.Clear();
    }

    public void SaveChanges()
    {
        if (!File.Exists(_filePath))
        {
            using var writer = new XmlTextWriter(_filePath, null);
            writer.WriteStartElement("Logs");
            writer.WriteEndElement();
        }
        var element = XElement.Load(_filePath);
        foreach (var log in _logs)
        {
            element.Add(new XElement("Log", log));
        }
        element.Save(_filePath);
    }

    public IList<string> Set()
    {
        return _logs;
    }
}

public class SqlLogContext : DbContext, ILogContext
{
    private readonly List<string> _logs = new();
    private readonly string _connectionString;

    public SqlLogContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<string>? Logs { get; set; }

    void ILogContext.SaveChanges()
    {
        foreach (var log in _logs)
        {
            Logs!.Add(log);
        }
        base.SaveChanges();
    }

    public override void Dispose()
    {
        _logs.Clear();
        base.Dispose();
    }

    public IList<string> Set()
    {
        return _logs;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}