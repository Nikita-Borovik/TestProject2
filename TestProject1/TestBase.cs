using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TestProjectCore;

namespace TestProject1
{
    [Parallelizable(ParallelScope.All)]
    public abstract class TestBase
    {
        protected IWebDriver driver => DriverManager.SetDriver();
        protected Serilog.ILogger Logger;
        private readonly string testId = Guid.NewGuid().ToString("N");
        private Stopwatch testStopwatch;

        [SetUp]
        public void Setup()
        {
            testStopwatch = Stopwatch.StartNew();
            Logger = LogManager.CreateLogger(testId);
            Logger.Information("===== TEST STARTED ({TestId}) =====", testId);
        }

        protected void LogStep(string message)
        {
            Logger.Information("STEP: " + message);
        }

        [TearDown]
        public void Teardown()
        {
            testStopwatch?.Stop();
            var testResult = TestContext.CurrentContext.Result;
            var status = testResult.Outcome.Status;
            var message = testResult.Message;
            var duration = testStopwatch?.Elapsed ?? TimeSpan.Zero;

            var testName = TestContext.CurrentContext.Test.Name;
            if (testName.Contains("("))
            {
                testName = testName.Substring(0, testName.IndexOf("("));
            }
            TestReportGenerator.RecordTestResult(
                testName,
                status,
                message,
                duration
            );

            if (status == TestStatus.Failed)
            {
                CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                Logger.Error("TEST FAILED: " + message);
            }

            Logger.Information("===== TEST FINISHED ({TestId}) =====", testId);
            Logger.Information("Test duration: {Duration} ms", duration.TotalMilliseconds);

            DriverManager.Teardown();
        }

        public void CaptureScreenshot(string name)
        {
            try
            {
                var ss = ((ITakesScreenshot)driver).GetScreenshot();
                var dir = "screenshots";

                Directory.CreateDirectory(dir);

                var path = $"{dir}/{name}_{testId}.png";
                ss.SaveAsFile(path);

                Logger.Error("Screenshot saved at: {Path}", path);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to take screenshot: " + ex.Message);
            }
        }
    }
}
