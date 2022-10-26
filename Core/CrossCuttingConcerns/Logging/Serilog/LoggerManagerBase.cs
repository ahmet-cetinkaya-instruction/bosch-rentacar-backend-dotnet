using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog;

public abstract class LoggerManagerBase : ILoggingManager
{
    protected ILogger _logger { get; set; }

    public virtual void Error(string message) => _logger.Error(message);
    public virtual void Fatal(string message) => _logger.Fatal(message);
    public virtual void Info(string message) => _logger.Information(message);
    public virtual void Warn(string message) => _logger.Warning(message);
    public virtual void Debug(string message) => _logger.Debug(message);
    public virtual void Verbose(string message) => _logger.Verbose(message);
}