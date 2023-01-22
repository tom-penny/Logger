namespace Logger;

public class LogTransaction : IDisposable
{
    private readonly ILogContext _context;

    public ILogRepository LogRepository { get; }

    public LogTransaction(ILogContext context)
    {
        _context = context;
        LogRepository = new LogRepository(context);
    }

    public void Complete()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}