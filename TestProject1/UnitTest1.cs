using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using TestProjectCore;
using TestProject1;
using Serilog;
using Shouldly;
using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using Allure.NUnit;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace TestProject
{

    [SetUpFixture]
    public class TestSuiteSetup
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            TestReportGenerator.SetSuiteStartTime();
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            TestReportGenerator.SetSuiteEndTime();
            TestReportGenerator.GenerateReport();
        }
    }

    [TestFixture]
    [AllureNUnit]
    public class Tests : TestBase
    {
        [Test]
        [Category("Regression")]
        [AllureTag("FailingTest")]
        public void Test2_Failing()
        {
            LogStep("Выполнение теста, который должен провалиться");
            
            Assert.That(2 + 2, Is.EqualTo(5), "Этот тест намеренно проваливается");
            Logger.Information("This should not be reached.");
        }

        [Test]
        [Category("Regression")]
        [AllureTag("InconclusiveTest")]
        public void Test3_Inconclusive()
        {
            LogStep("Выполнение теста со статусом Inconclusive");
            
            Assert.Inconclusive("Этот тест помечен как Inconclusive для демонстрации");
            Logger.Information("This should not be reached.");
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
