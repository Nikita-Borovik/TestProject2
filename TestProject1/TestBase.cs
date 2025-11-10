using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TestProjectCore;

namespace TestProject1
{
    [Parallelizable(ParallelScope.All)]
    public abstract class TestBase
    {
        protected IWebDriver driver => DriverManager.SetDriver();

        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void Teardown()
        {
            DriverManager.Teardown();
        }
    }
}
