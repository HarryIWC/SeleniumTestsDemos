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
    public class ResetAppState
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
        public void ResetAppAndLogout()
        {
            IWebElement usernameField = Driver.FindElement(By.Id("user-name"));
            usernameField.SendKeys("standard_user");

            IWebElement passwordField = Driver.FindElement(By.Id("password"));
            passwordField.SendKeys("secret_sauce");

            IWebElement loginButton = Driver.FindElement(By.Id("login-button"));
            loginButton.Click();

            //Confirmation for a successful login
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("span[class='title']")));

            IWebElement filterButton = Driver.FindElement(By.ClassName("product_sort_container"));

            SelectElement filterType = new SelectElement(filterButton);
            filterType.SelectByIndex(2);
               
            IWebElement itemTshirt = Driver.FindElement(By.Id("add-to-cart-sauce-labs-bolt-t-shirt"));
            itemTshirt.Click();

            //Confirmation that the cart containts one product (kind of)
            IWebElement numsInCartButton = Driver.FindElement(By.ClassName("shopping_cart_badge"));
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(numsInCartButton, "1"));

            IWebElement burgerMenuButton = Driver.FindElement(By.Id("react-burger-menu-btn")); 
            burgerMenuButton.Click();

            IWebElement resetAppStateButton = Driver.FindElement(By.Id("reset_sidebar_link"));
            resetAppStateButton.Click();

            Assert.True(numsInCartButton != null);         
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Close();
        }
    }
}
