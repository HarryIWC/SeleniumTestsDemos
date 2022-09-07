using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AutomationTestingOne
{
    [TestFixture]
    public class UsingFilters
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
        public void FilterTheItems()
        {
            IWebElement usernameField = Driver.FindElement(By.Id("user-name"));
            usernameField.SendKeys("standard_user");

            IWebElement passwordField = Driver.FindElement(By.Id("password"));
            passwordField.SendKeys("secret_sauce");

            IWebElement loginButton = Driver.FindElement(By.Id("login-button"));
            loginButton.Click();

            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("span[class='title']")));

            IWebElement filterButton = Driver.FindElement(By.ClassName("product_sort_container"));

            SelectElement filterType = new SelectElement(filterButton);
            filterType.SelectByIndex(2);

            List<IWebElement> productList = Driver.FindElements(By.ClassName("inventory_item_price")).ToList();
            var first = productList[0].Text;
            var sixth = productList[5].Text;

            Assert.AreEqual(first, "$7.99");
            Assert.AreEqual(sixth, "$49.99");
        }
        [TearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}