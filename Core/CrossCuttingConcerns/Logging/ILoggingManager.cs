namespace Core.CrossCuttingConcerns.Logging;

public interface ILoggingManager
{
    public void Error(string message);
    public void Fatal(string message);
    public void Info(string message);
    public void Warn(string message);
    public void Debug(string message);
    public void Verbose(string message);
}