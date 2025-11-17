using Serilog;

namespace TestProject1
{
    public static class LogManager
    {
        public static ILogger CreateLogger(string testId)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithThreadId()
                .Enrich.WithProperty("TestRunId", testId)
                .WriteTo.Console(outputTemplate:
                    "[{Timestamp:HH:mm:ss}] [T{ThreadId}] [{Level}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    $"logs/test_{testId}.log",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10_000_000,
                    retainedFileCountLimit: 10,
                    rollOnFileSizeLimit: true,
                    outputTemplate:
                    "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [T{ThreadId}] [{Level}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
