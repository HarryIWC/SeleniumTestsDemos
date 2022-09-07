using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTestingOne
{
    [TestFixture]
    public class LoginWithInvalidUsername
    {
        IWebDriver Driver;
        WebDriverWait Wait;

        [SetUp]
        public void TestSetup()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void LoginForm()
        {
            IWebElement usernameField = Driver.FindElement(By.Id("user-name"));
            usernameField.SendKeys("locked_out_user");

            IWebElement passwordField = Driver.FindElement(By.Id("password"));
            passwordField.SendKeys("secret_sauce");

            IWebElement loginButton = Driver.FindElement(By.Id("login-button"));
            loginButton.Click();

            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("h3[data-test='error']")));
            IWebElement confirmTheError = Driver.FindElement(By.CssSelector("h3[data-test='error']"));
            Assert.AreEqual(confirmTheError.Text, "Epic sadface: Sorry, this user has been locked out.");
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
