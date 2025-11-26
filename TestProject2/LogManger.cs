using Serilog;
using Xunit.Abstractions;

namespace TestProject2
{
    public static class LogManager
    {
        public static ILogger CreateLogger(string testId, ITestOutputHelper? testOutput = null)
        {
            var logsDirectory = "logs";
            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }

            if (testOutput != null)
            {
                XUnitTestOutputSink.SetTestOutput(testOutput);
            }

            var config = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithThreadId()
                .Enrich.WithProperty("TestRunId", testId)
                .WriteTo.File(
                    Path.Combine(logsDirectory, $"test_{testId}.log"),
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10_000_000,
                    retainedFileCountLimit: 10,
                    rollOnFileSizeLimit: true,
                    outputTemplate:
                    "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [T{ThreadId}] [{Level}] {Message:lj}{NewLine}{Exception}");

            if (testOutput != null)
            {
                config.WriteTo.Sink(new XUnitTestOutputSink(
                    "[{Timestamp:HH:mm:ss}] [T{ThreadId}] [{Level}] {Message:lj}{NewLine}{Exception}"));
            }
            else
            {
                config.WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss}] [T{ThreadId}] [{Level}] {Message:lj}{NewLine}{Exception}")
                    .WriteTo.Debug(
                        outputTemplate: "[{Timestamp:HH:mm:ss}] [T{ThreadId}] [{Level}] {Message:lj}{NewLine}{Exception}");
            }

            return config.CreateLogger();
        }
    }
}
