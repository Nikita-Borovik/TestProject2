using xUnit;
using TestProject2;
using Shouldly;
using Xunit.Abstractions;

[assembly: CollectionBehavior()]

namespace xUnit
{
    [Collection("Our Test Collection #1")]
    public class Tests : TestBase
    {
        public Tests(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("https://en.ehuniversity.lt/about/", "About")]
        public void Test1(string expectedTitleUrl, string expectedTitle)
        {
            var mainPage = new MainPage(driver);

            LogStep("Opening Home Page");
            mainPage.OpenHomePage();

            LogStep("Navigating to About Page");
            mainPage.OpenAboutPage();

            LogStep("Validating URL and Title");

            try
            {
                driver.Url.ShouldBe(expectedTitleUrl);
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

    [Collection("Our Test Collection #2")]
    public class Tests2 : TestBase
    {
        public Tests2(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("/?s=study+programs")]
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

    [Collection("Our Test Collection #3")]
    public class Tests3 : TestBase
    {
        public Tests3(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("https://lt.ehuniversity.lt/")]
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

    [Collection("Our Test Collection #4")]
    public class Tests4 : TestBase
    {
        public Tests4(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [Trait("Category", "Smoke")]
        [InlineData("E-mail: franciskscarynacr@gmail.com", "Phone (LT): +370 68 771365", "Phone (BY): +375 29 5781488", "Join us in the social networks: Facebook Telegram VK")]
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
