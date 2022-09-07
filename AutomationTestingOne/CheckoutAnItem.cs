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
    public class CheckoutAnItem
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
        public void CheckoutAnItemE2E()
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
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("inventory_item_name")));

            IWebElement checkoutButton = Driver.FindElement(By.Id("checkout"));
            checkoutButton.Click();

            IWebElement checkoutPageConfirmation = Driver.FindElement(By.ClassName("title"));
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(checkoutPageConfirmation, "CHECKOUT: YOUR INFORMATION"));

            IWebElement firstName = Driver.FindElement(By.Id("first-name"));
            firstName.SendKeys("Harry");

            IWebElement lastName = Driver.FindElement(By.Id("last-name"));
            lastName.SendKeys("Milosheski");

            IWebElement postalCode = Driver.FindElement(By.Id("postal-code"));
            postalCode.SendKeys("7500");

            IWebElement continueToCheckoutButton = Driver.FindElement(By.Id("continue"));
            continueToCheckoutButton.Click();

            IWebElement totalMoneyConfirmation = Driver.FindElement(By.ClassName("summary_total_label"));
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(totalMoneyConfirmation, "Total: $"));

            IWebElement finishButton = Driver.FindElement(By.Id("finish"));
            finishButton.Click();

            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("span[class='title']")));

            IWebElement confirmThePurchase = Driver.FindElement(By.ClassName("complete-header"));
            Assert.AreEqual(confirmThePurchase.Text, "THANK YOU FOR YOUR ORDER");         
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
