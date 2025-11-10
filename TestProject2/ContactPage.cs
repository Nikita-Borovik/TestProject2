using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TestProject2
{
    internal class ContactPage
    {
        private readonly IWebDriver driver;

        public ContactPage(IWebDriver driver) => this.driver = driver;

        private IWebElement eMail => driver.FindElement(By.CssSelector("li[plerdy-tracking-id='35448735101']"));

        private IWebElement phoneLT => driver.FindElement(By.CssSelector("li[plerdy-tracking-id='50296369501']"));

        private IWebElement phoneBY => driver.FindElement(By.CssSelector("li[plerdy-tracking-id='39744896801']"));

        private IWebElement socialNet => driver.FindElement(By.CssSelector("li[plerdy-tracking-id='64965466401']"));

        public void OpenContactPage()
        {
            driver.Navigate().GoToUrl("https://en.ehu.lt/contact/");
        }

        public string GetEmail() => eMail.Text;
        public string GetPhoneLT() => phoneLT.Text;
        public string GetPhoneBY() => phoneBY.Text;
        public string GetSocialNet() => socialNet.Text;
    }
}
