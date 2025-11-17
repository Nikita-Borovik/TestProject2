using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using TestProjectCore;
using TestProject1;
using Serilog;
using Shouldly;

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

            LogStep("Opening Home Page");
            mainPage.OpenHomePage();

            LogStep("Navigating to About Page");
            mainPage.OpenAboutPage();

            LogStep("Validating URL and Title");

            try
            {
                driver.Url.ShouldBe(expectedUrl);
                driver.Title.ShouldBe(expectedTitle);
                Logger.Information("Test passed.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Test failed!");
                CaptureScreenshot("Test1_Failure");
                throw;
            }
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

            LogStep("Opening Home Page");
            mainPage.OpenHomePage();

            LogStep("Make Searching Request");
            mainPage.OpenSearchingRequest(searchingRequest);

            LogStep("Submit Searching Form");
            mainPage.ClickSubmitButton();

            LogStep("Validating Searching Page");

            try
            {
                driver.Url.ShouldContain(expectedPart);
                Logger.Information("Test passed.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Test failed!");
                CaptureScreenshot("Test2_Failure");
                throw;
            }
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

            LogStep("Opening Home Page");
            mainPage.OpenHomePage();

            LogStep("Change Language To Lithuanian");
            mainPage.ChangeLanguageToLT();

            LogStep("Validating URL");

            try
            {
                driver.Url.ShouldBe(expectedUrl);
                Logger.Information("Test passed.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Test failed!");
                CaptureScreenshot("Test3_Failure");
                throw;
            }
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

            LogStep("Opening Contact Page");
            contactPage.OpenContactPage();

            LogStep("Validating contact information");

            try
            {
                contactPage.GetEmail().ShouldBe(email);
                Logger.Information("Test passed.");
                contactPage.GetPhoneLT().ShouldBe(phoneLt);
                Logger.Information("Test passed.");
                contactPage.GetPhoneBY().ShouldBe(phoneBy);
                Logger.Information("Test passed.");
                contactPage.GetSocialNet().ShouldBe(social);
                Logger.Information("Test passed.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Test failed!");
                CaptureScreenshot("Test4_Failure");
                throw;
            }
        }
    }

}
