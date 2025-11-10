using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using TestProjectCore;
using TestProject1;
[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace TestProject
{

    [TestFixture]
    public class Tests : TestBase
    {
        [Test]
        [Category("Regression")]
        [TestCase("https://en.ehuniversity.lt/about/", "About")]
        public void Test1(string expectedUrl, string expectedTitle)
        {
            var mainPage = new MainPage(driver);

            mainPage.OpenHomePage();
            mainPage.OpenAboutPage();

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(expectedUrl));
                Assert.That(driver.Title, Is.EqualTo(expectedTitle));
            });
        }
    }

    [TestFixture]
    public class Tests2 : TestBase
    {
        [Test]
        [Category("Regression")]
        [TestCase("/?s=study+programs")]
        public void Test2(string expectedPart)
        {
            var mainPage = new MainPage(driver);
            var searchingRequest = "study programs";

            mainPage.OpenHomePage();
            mainPage.OpenSearchingRequest(searchingRequest);
            mainPage.ClickSubmitButton();

            Assert.That(driver.Url, Does.Contain(expectedPart));
        }
    }

    [TestFixture]
    public class Tests3 : TestBase
    {
        [Test]
        [Category("Regression")]
        [TestCase("https://lt.ehuniversity.lt/")]
        public void Test3(string expectedUrl)
        {
            var mainPage = new MainPage(driver);

            mainPage.OpenHomePage();
            mainPage.ChangeLanguageToLT();

            Assert.That(driver.Url, Is.EqualTo(expectedUrl));
        }
    }

    [TestFixture]
    public class Tests4 : TestBase
    {
        [Test]
        [Category("Regression")]
        [TestCase("E-mail: franciskscarynacr@gmail.com", "Phone (LT): +370 68 771365", "Phone (BY): +375 29 5781488", "Join us in the social networks: Facebook Telegram VK")]
        public void Test4(string email, string phoneLt, string phoneBy, string social)
        {
            var contactPage = new ContactPage(driver);

            contactPage.OpenContactPage();

            Assert.Multiple(() =>
            {
                Assert.That(contactPage.GetEmail, Is.EqualTo(email));
                Assert.That(contactPage.GetPhoneLT, Is.EqualTo(phoneLt));
                Assert.That(contactPage.GetPhoneBY, Is.EqualTo(phoneBy));
                Assert.That(contactPage.GetSocialNet, Is.EqualTo(social));
            });
        }
    }

}
