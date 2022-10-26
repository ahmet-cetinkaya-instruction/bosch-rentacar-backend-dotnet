using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog;

public class FileLogger : LoggerManagerBase
{
    private FileLogConfiguration _fileLogConfiguration;

    public FileLogger(IConfiguration configuration)
    {
        _fileLogConfiguration = configuration.GetSection("LogConfigurations:FileLogConfiguration")
                                             .Get<FileLogConfiguration>();

        string logFilePath = $"{Directory.GetCurrentDirectory()}{_fileLogConfiguration.FolderPath}.txt";

        _logger = new LoggerConfiguration()
                  .WriteTo.File(logFilePath,
                                rollingInterval: RollingInterval.Day,
                                retainedFileCountLimit: null,
                                fileSizeLimitBytes: 5000000,
                                outputTemplate:
                                "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message} {NewLine}{Exception}"
                  )
                  .CreateLogger();
    }
}