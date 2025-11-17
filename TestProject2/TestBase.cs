using OpenQA.Selenium;
using Serilog;
using TestProject2;
using Xunit.Abstractions;

namespace TestProject2
{
    public abstract class TestBase : IDisposable
    {
        protected IWebDriver driver => DriverManager.SetDriver();
        protected ILogger Logger { get; private set; }
        private readonly string testId = Guid.NewGuid().ToString("N");

        protected TestBase(ITestOutputHelper? testOutput = null)
        {
            Logger = LogManager.CreateLogger(testId, testOutput);
            Logger.Information("===== TEST STARTED ({TestId}) =====", testId);
        }

        protected void LogStep(string message)
        {
            Logger.Information("STEP: " + message);
        }

        protected void CaptureScreenshot(string name)
        {
            try
            {
                if (driver is ITakesScreenshot screenshotDriver)
                {
                    var directory = "screenshots";
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    var fileName = Path.Combine(directory, $"{name}_{testId}.png");

                    var screenshot = screenshotDriver.GetScreenshot();
                    screenshot.SaveAsFile(fileName);

                    Logger.Error("Screenshot saved at: {Path}", fileName);
                }
                else
                {
                    Logger.Warning("Driver does not support screenshots.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to capture screenshot");
            }
        }

        public virtual void Dispose()
        {
            Logger.Information("===== TEST FINISHED ({TestId}) =====", testId);
            DriverManager.Teardown();
        }
    }
}


