using xUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject2;
[assembly: CollectionBehavior()]

namespace xUnit
{
    [Collection("Our Test Collection #1")]
    public class Tests : IDisposable
    {
        private static IWebDriver driver => DriverManager.SetDriver();

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("https://en.ehuniversity.lt/about/", "About")]
        public void Test1(string expectedTitleUrl, string expectedTitle)
        {
            var mainPage = new MainPage(driver);

            mainPage.OpenHomePage();
            mainPage.OpenAboutPage();

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
        private static IWebDriver driver => DriverManager.SetDriver();

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("/?s=study+programs")]
        public void Test2(string expectedPart)
        {
            var mainPage = new MainPage(driver);
            var searchingRequest = "study programs";

            mainPage.OpenHomePage();
            mainPage.OpenSearchingRequest(searchingRequest);
            mainPage.ClickSubmitButton();

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
        private static IWebDriver driver => DriverManager.SetDriver();

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("https://lt.ehuniversity.lt/")]
        public void Test3(string expectedUrl)
        {
            var mainPage = new MainPage(driver);

            mainPage.OpenHomePage();
            mainPage.ChangeLanguageToLT();

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
        private static IWebDriver driver => DriverManager.SetDriver();

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("E-mail: franciskscarynacr@gmail.com", "Phone (LT): +370 68 771365", "Phone (BY): +375 29 5781488", "Join us in the social networks: Facebook Telegram VK")]
        public void Test4(string email, string phoneLt, string phoneBy, string social)
        {
            var contactPage = new ContactPage(driver);

            contactPage.OpenContactPage();

            Assert.Multiple(() =>
            {
                Assert.Equal(email, contactPage.GetEmail());
                Assert.Equal(phoneLt, contactPage.GetPhoneLT());
                Assert.Equal(phoneBy, contactPage.GetPhoneBY());
                Assert.Equal(social, contactPage.GetSocialNet());
            });
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
