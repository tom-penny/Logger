namespace Logger;

public interface ILogRepository
{
    void Insert(string message);
}

public class LogRepository : ILogRepository
{
    private readonly ILogContext _context;

    public LogRepository(ILogContext context)
    {
        _context = context;
    }

    public void Insert(string message)
    {
        _context.Set().Add(message);
    }
}