using System;
using System.Collections.Generic;
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

        [SetUp]
        public void Setup()
        {
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
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                Logger.Error("TEST FAILED: " + TestContext.CurrentContext.Result.Message);
            }

            Logger.Information("===== TEST FINISHED ({TestId}) =====", testId);

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
