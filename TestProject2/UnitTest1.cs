using xUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
[assembly: CollectionBehavior()]

namespace xUnit
{
    [Collection("Our Test Collection #1")]
    public class Tests : IDisposable
    {
        private static IWebDriver driver;

        public Tests()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");

            driver = new ChromeDriver(options);
        }

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("https://en.ehuniversity.lt/about/", "About")]
        public void Test1(string expectedTitleUrl, string expectedTitle)
        {
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");

            IWebElement toggleMenu = driver.FindElement(By.CssSelector("div[id='toggle-menu']"));
            toggleMenu.Click();

            IWebElement aboutButton = driver.FindElement(By.CssSelector("li[id='menu-item-16178']"));
            aboutButton.Click();

            IWebElement title = driver.FindElement(By.XPath("//title"));

            Assert.Multiple(() =>
            {
                Assert.Equal(expectedTitleUrl, driver.Url);
                Assert.Equal(expectedTitle, driver.Title);
            });
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
    [Collection("Our Test Collection #2")]
    public class Tests2 : IDisposable
    {
        private static IWebDriver driver;
        public Tests2()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");

            driver = new ChromeDriver(options);
        }

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("/?s=study+programs")]
        public void Test2(string expectedPart)
        {
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");

            IWebElement headerSearch = driver.FindElement(By.CssSelector("div[class='header-search']"));
            headerSearch.Click();

            IWebElement searchField = driver.FindElement(By.CssSelector("input[class='form-control']"));
            searchField.SendKeys("study programs");

            IWebElement submitButton = driver.FindElement(By.CssSelector("button[type=submit]"));
            submitButton.Click();

            Assert.Contains(expectedPart, driver.Url);
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
    [Collection("Our Test Collection #3")]
    public class Tests3 : IDisposable
    {
        private static IWebDriver driver;
        public Tests3()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");

            driver = new ChromeDriver(options);
        }

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("https://lt.ehuniversity.lt/")]
        public void Test3(string expectedUrl)
        {
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");

            IWebElement langSwitcher = driver.FindElement(By.CssSelector("li[onclick]"));
            langSwitcher.Click();

            IWebElement ltLangButton = driver.FindElement(By.CssSelector("li[plerdy-tracking-id='39047282601']"));
            ltLangButton.Click();

            Assert.Equal(expectedUrl, driver.Url);
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }

    [Collection("Our Test Collection #4")]
    public class Tests4 : IDisposable
    {
        private static IWebDriver driver;
        public Tests4()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");

            driver = new ChromeDriver(options);
        }

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("E-mail: franciskscarynacr@gmail.com", "Phone (LT): +370 68 771365", "Phone (BY): +375 29 5781488", "Join us in the social networks: Facebook Telegram VK")]
        public void Test4(string email, string phoneLt, string phoneBy, string social)
        {
            driver.Navigate().GoToUrl("https://en.ehu.lt/contact/");

            IWebElement eMail = driver.FindElement(By.CssSelector("li[plerdy-tracking-id='35448735101']"));

            IWebElement phoneLT = driver.FindElement(By.CssSelector("li[plerdy-tracking-id='50296369501']"));

            IWebElement phoneBY = driver.FindElement(By.CssSelector("li[plerdy-tracking-id='39744896801']"));

            IWebElement socialNet = driver.FindElement(By.CssSelector("li[plerdy-tracking-id='64965466401']"));


            Assert.Equal(email, eMail.Text);
            Assert.Equal(phoneLt, phoneLT.Text);
            Assert.Equal(phoneBy, phoneBY.Text);
            Assert.Equal(social, socialNet.Text);
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }


}
