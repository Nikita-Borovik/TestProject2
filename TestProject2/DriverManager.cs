using DiffEngine;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject2
{
    public sealed class DriverManager
    {
        private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        private DriverManager() { }

        public static IWebDriver SetDriver()
        {
            if (driver.Value == null)
            {
                var options = new ChromeOptions();
                options.AddArgument("--headless=new");

                driver.Value = new ChromeDriver(options);
            }
            return driver.Value;
        }

        public static void Teardown()
        {
            driver.Value?.Quit();
            driver.Value?.Dispose();
        }
    }
}
