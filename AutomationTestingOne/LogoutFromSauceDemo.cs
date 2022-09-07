﻿using NUnit.Framework;
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
    public class LogoutFromSauceDemo
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
        public void Logout()
        {
            IWebElement usernameField = Driver.FindElement(By.Id("user-name"));
            usernameField.SendKeys("standard_user");

            IWebElement passwordField = Driver.FindElement(By.Id("password"));
            passwordField.SendKeys("secret_sauce");

            IWebElement loginButton = Driver.FindElement(By.Id("login-button"));
            loginButton.Click();

            IWebElement burgerMenu = Driver.FindElement(By.Id("react-burger-menu-btn"));
            burgerMenu.Click();

            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("logout_sidebar_link")));
            IWebElement logoutButton = Driver.FindElement(By.Id("logout_sidebar_link"));
            logoutButton.Click();

            Assert.AreEqual("https://www.saucedemo.com/", Driver.Url);
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
