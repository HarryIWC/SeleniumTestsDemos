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
    public class AddAnItemToChart
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
        public void AddAnItem()
        {
            IWebElement usernameField = Driver.FindElement(By.Id("user-name"));
            usernameField.SendKeys("standard_user");

            IWebElement passwordField = Driver.FindElement(By.Id("password"));
            passwordField.SendKeys("secret_sauce");

            IWebElement loginButton = Driver.FindElement(By.Id("login-button"));
            loginButton.Click();

            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("span[class='title']")));

            IWebElement itemTshirt = Driver.FindElement(By.Id("add-to-cart-sauce-labs-bolt-t-shirt"));
            itemTshirt.Click();

            IWebElement cartButton = Driver.FindElement(By.ClassName("shopping_cart_link"));
            cartButton.Click();

            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("https://www.saucedemo.com/cart.html"));

            IWebElement confirmtheAddition = Driver.FindElement(By.ClassName("inventory_item_name"));
            Assert.AreEqual(confirmtheAddition.Text, "Sauce Labs Bolt T-Shirt");
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Close(); 
        }
    }
}
