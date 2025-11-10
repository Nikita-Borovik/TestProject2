using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TestProject1
{
    internal class MainPage
    {
        private readonly IWebDriver driver;

        public MainPage(IWebDriver driver) => this.driver = driver;

        private IWebElement toggleMenu => driver.FindElement(By.CssSelector("div[id='toggle-menu']"));
        private IWebElement aboutButton => driver.FindElement(By.CssSelector("li[id='menu-item-16178']"));
        private IWebElement headerSearch => driver.FindElement(By.CssSelector("div[class='header-search']"));
        private IWebElement searchField => driver.FindElement(By.CssSelector("input[class='form-control']"));
        private IWebElement submitButton => driver.FindElement(By.CssSelector("button[type=submit]"));
        private IWebElement langSwitcher => driver.FindElement(By.CssSelector("li[onclick]"));
        private IWebElement ltLangButton => driver.FindElement(By.CssSelector("li[plerdy-tracking-id='39047282601']"));

        public void OpenHomePage()
        {
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");
        }

        public void OpenAboutPage()
        {
            toggleMenu.Click();
            aboutButton.Click();
        }

        public void OpenSearchingRequest(string searchingRequest)
        {
            headerSearch.Click();
            searchField.SendKeys(searchingRequest);
        }
        
        public void ClickSubmitButton()
        {
            submitButton.Click();
        }

        public void ChangeLanguageToLT()
        {
            langSwitcher.Click();
            ltLangButton.Click();
        }
    }
}
