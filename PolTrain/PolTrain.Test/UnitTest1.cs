using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Glimpse.AspNet.Tab;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Environment = System.Environment;


namespace PolTrain.Test
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver _driver;
        [TestInitialize]
        public void TestInit()
        {

            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl("http://localhost:10402/");
        }
        [TestMethod]
        public void TestMethod1()
        {
            var EmailInput = _driver.FindElement(By.Id("Email"));
            var PasswordInput = _driver.FindElement(By.Id("Password"));
            EmailInput.SendKeys("adam.dlubak@gmail.com");
            PasswordInput.SendKeys("PolTrain!");
            PasswordInput.SendKeys(Keys.Enter);
            Assert.AreEqual("abc", "abc");

        }


        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
        }
 
    }
}
