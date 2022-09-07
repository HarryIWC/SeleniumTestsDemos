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
    public class AddingItemsAndDeletingOne
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
        public void AddAndDelete()
        {
            IWebElement usernameField = Driver.FindElement(By.Id("user-name"));
            usernameField.SendKeys("standard_user");

            IWebElement passwordField = Driver.FindElement(By.Id("password"));
            passwordField.SendKeys("secret_sauce");

            IWebElement loginButton = Driver.FindElement(By.Id("login-button"));
            loginButton.Click();

            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("span[class='title']")));

            IWebElement itemTshirtOne = Driver.FindElement(By.Id("add-to-cart-sauce-labs-bolt-t-shirt"));
            itemTshirtOne.Click();

            IWebElement itemTshirtTwo = Driver.FindElement(By.Id("add-to-cart-sauce-labs-fleece-jacket"));
            itemTshirtTwo.Click();

            IWebElement itemTshirtThree = Driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            itemTshirtThree.Click();

            IWebElement cartButton = Driver.FindElement(By.ClassName("shopping_cart_link"));
            cartButton.Click();

            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("https://www.saucedemo.com/cart.html"));
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("remove-sauce-labs-bolt-t-shirt")));
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("remove-sauce-labs-fleece-jacket")));
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("remove-sauce-labs-backpack")));

            IWebElement deleteProduct = Driver.FindElement(By.Id("remove-sauce-labs-bolt-t-shirt"));
            deleteProduct.Click();

            var cartQuantity = Driver.FindElements(By.ClassName("cart_item"));
            Assert.AreEqual(cartQuantity.Count(), 2);
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Close();
        }
    }
}
