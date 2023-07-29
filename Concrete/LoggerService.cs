using autolog.Abstract;

namespace autolog.Concrete;

public class LoggerService
{
    private readonly ILog _logger;
    public LoggerService(ILog logger)
    {
        _logger = logger;
    }

    public string PrintLog(string message)
    {
        return _logger.Log(message);
    }
}