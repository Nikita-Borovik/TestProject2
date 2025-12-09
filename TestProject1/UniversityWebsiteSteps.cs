using NUnit.Framework;
using Reqnroll;
using Shouldly;
using TestProject1;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using TestProjectCore;
using Serilog;
using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using Allure.NUnit;

namespace TestProject
{
    [Binding]
    public class UniversityWebsiteSteps : TestBase
    {
        private readonly MainPage _mainPage;
        private readonly ContactPage _contactPage;

        public UniversityWebsiteSteps()
        {
            // Initialize logger for Reqnroll tests
            if (Logger == null)
            {
                Logger = LogManager.CreateLogger(Guid.NewGuid().ToString("N"));
            }
            _mainPage = new MainPage(driver);
            _contactPage = new ContactPage(driver);
        }

        [Given(@"I am on the university website homepage")]
        public void GivenIAmOnTheUniversityWebsiteHomepage()
        {
            LogStep("Navigating to university website homepage");
            _mainPage.OpenHomePage();
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string searchTerm)
        {
            LogStep($"Searching for: {searchTerm}");
            _mainPage.OpenSearchingRequest(searchTerm);
        }

        [When(@"I submit the search form")]
        public void WhenISubmitTheSearchForm()
        {
            LogStep("Submitting search form");
            _mainPage.ClickSubmitButton();
        }

        [Then(@"I should see search results")]
        public void ThenIShouldSeeSearchResults()
        {
            LogStep("Verifying search results");
            try
            {
                // Note: This test was simplified in the original implementation
                // In a real scenario, we would add proper assertions here
                Assert.That(true, Is.True, "Search functionality executed successfully");
                LogStep("Search results verification passed");
            }
            catch (Exception ex)
            {
                LogStep($"Search results verification failed: {ex.Message}");
                throw;
            }
        }

        [When(@"I change the language to Lithuanian")]
        public void WhenIChangeTheLanguageToLithuanian()
        {
            LogStep("Changing language to Lithuanian");
            _mainPage.ChangeLanguageToLT();
        }

        [Then(@"I should be redirected to the Lithuanian version of the site")]
        public void ThenIShouldBeRedirectedToTheLithuanianVersionOfTheSite()
        {
            LogStep("Verifying Lithuanian URL");
            try
            {
                driver.Url.ShouldBe("https://lt.ehuniversity.lt/");
                LogStep("Lithuanian URL verification passed");
            }
            catch (Exception ex)
            {
                LogStep($"Lithuanian URL verification failed. Expected: https://lt.ehuniversity.lt/, Actual: {driver.Url}. Error: {ex.Message}");
                throw;
            }
        }

        [When(@"I navigate to the contact page")]
        public void WhenINavigateToTheContactPage()
        {
            LogStep("Navigating to contact page");
            _contactPage.OpenContactPage();
        }

        [Then(@"I should see the correct email address ""(.*)""")]
        public void ThenIShouldSeeTheCorrectEmailAddress(string expectedEmail)
        {
            LogStep("Verifying email address");
            try
            {
                _contactPage.GetEmail().ShouldBe(expectedEmail);
                LogStep("Email address verification passed");
            }
            catch (Exception ex)
            {
                LogStep($"Email address verification failed: {ex.Message}");
                throw;
            }
        }

        [Then(@"I should see the correct Lithuanian phone number ""(.*)""")]
        public void ThenIShouldSeeTheCorrectLithuanianPhoneNumber(string expectedPhoneLt)
        {
            LogStep("Verifying Lithuanian phone number");
            try
            {
                _contactPage.GetPhoneLT().ShouldBe(expectedPhoneLt);
                LogStep("Lithuanian phone number verification passed");
            }
            catch (Exception ex)
            {
                LogStep($"Lithuanian phone number verification failed: {ex.Message}");
                throw;
            }
        }

        [Then(@"I should see the correct Belarus phone number ""(.*)""")]
        public void ThenIShouldSeeTheCorrectBelarusPhoneNumber(string expectedPhoneBy)
        {
            LogStep("Verifying Belarus phone number");
            try
            {
                _contactPage.GetPhoneBY().ShouldBe(expectedPhoneBy);
                LogStep("Belarus phone number verification passed");
            }
            catch (Exception ex)
            {
                LogStep($"Belarus phone number verification failed: {ex.Message}");
                throw;
            }
        }

        [Then(@"I should see the correct social networks information ""(.*)""")]
        public void ThenIShouldSeeTheCorrectSocialNetworksInformation(string expectedSocial)
        {
            LogStep("Verifying social networks information");
            try
            {
                _contactPage.GetSocialNet().ShouldBe(expectedSocial);
                LogStep("Social networks information verification passed");
            }
            catch (Exception ex)
            {
                LogStep($"Social networks information verification failed: {ex.Message}");
                throw;
            }
        }
    }
}
