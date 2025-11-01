using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace TestProject
{
    [TestFixture]
    public class Tests
    {
        private ChromeDriver driver;

        [SetUp]
        public void SetUpTest()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            driver = new ChromeDriver(options);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        [Category("Regression")]
        [TestCase("https://en.ehuniversity.lt/about/", "About")]
        public void Test1(string expectedUrl, string expectedTitle)
        {
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");

            IWebElement toggleMenu = driver.FindElement(By.CssSelector("div[id='toggle-menu']"));
            toggleMenu.Click();

            IWebElement aboutButton = driver.FindElement(By.CssSelector("li[id='menu-item-16178']"));
            aboutButton.Click();

            IWebElement title = driver.FindElement(By.XPath("//title"));

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(expectedUrl));
                Assert.That(driver.Title, Is.EqualTo(expectedTitle));
            });
        }
    }
    [TestFixture]
    public class Tests2
    {
        private ChromeDriver driver;

        [SetUp]
        public void SetUpTest()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            driver = new ChromeDriver(options);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        [Category("Regression")]
        [TestCase("/?s=study+programs")]
        public void Test2(string expectedPart)
        {
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");

            IWebElement headerSearch = driver.FindElement(By.CssSelector("div[class='header-search']"));
            headerSearch.Click();

            IWebElement searchField = driver.FindElement(By.CssSelector("input[class='form-control']"));
            searchField.SendKeys("study programs");

            IWebElement submitButton = driver.FindElement(By.CssSelector("button[type=submit]"));
            submitButton.Click();

            Assert.That(driver.Url, Does.Contain(expectedPart));
        }
    }

    [TestFixture]
    public class Tests3
    {
        private ChromeDriver driver;

        [SetUp]
        public void SetUpTest()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            driver = new ChromeDriver(options);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        [Category("Regression")]
        [TestCase("https://lt.ehuniversity.lt/")]
        public void Test3(string expectedUrl)
        {
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");

            IWebElement langSwitcher = driver.FindElement(By.CssSelector("li[onclick]"));
            langSwitcher.Click();

            IWebElement ltLangButton = driver.FindElement(By.CssSelector("li[plerdy-tracking-id='39047282601']"));
            ltLangButton.Click();

            Assert.That(driver.Url, Is.EqualTo(expectedUrl));
        }
    }

    [TestFixture]
    public class Tests4
    {
        private ChromeDriver driver;

        [SetUp]
        public void SetUpTest1()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            driver = new ChromeDriver(options);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        [Category("Regression")]
        [TestCase("E-mail: franciskscarynacr@gmail.com", "Phone (LT): +370 68 771365", "Phone (BY): +375 29 5781488", "Join us in the social networks: Facebook Telegram VK")]
        public void Test4(string email, string phoneLt, string phoneBy, string social)
        {
            driver.Navigate().GoToUrl("https://en.ehu.lt/contact/");

            IWebElement eMail = driver.FindElement(By.CssSelector("li[plerdy-tracking-id='35448735101']"));

            IWebElement phoneLT = driver.FindElement(By.CssSelector("li[plerdy-tracking-id='50296369501']"));

            IWebElement phoneBY = driver.FindElement(By.CssSelector("li[plerdy-tracking-id='39744896801']"));

            IWebElement socialNet = driver.FindElement(By.CssSelector("li[plerdy-tracking-id='64965466401']"));

            Assert.Multiple(() =>
            {
                Assert.That(eMail.Text, Is.EqualTo(email));
                Assert.That(phoneLT.Text, Is.EqualTo(phoneLt));
                Assert.That(phoneBY.Text, Is.EqualTo(phoneBy));
                Assert.That(socialNet.Text, Is.EqualTo(social));
            });
        }
    }

}
