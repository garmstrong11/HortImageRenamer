namespace HortImageRenamer.Logging
{
  using HortImageRenamer.Domain;

  public class NlogLoggerAdapter : ILogger
  {
    private readonly NLog.ILogger _logger;

    public NlogLoggerAdapter(NLog.ILogger logger)
    {
      _logger = logger;
    }

    #region Implementation of ILogger

    public void Info(string message)
    {
      _logger.Info(message);
    }

    public void Warn(string message)
    {
      _logger.Warn(message);
    }

    public void Error(string message)
    {
      _logger.Error(message);
    }

    public void Fatal(string message)
    {
      _logger.Fatal(message);
    }

    #endregion
  }
}