using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void QuitDriver()
        {
            driver.Value?.Quit();
            driver.Value?.Dispose();
        }
    }
}
