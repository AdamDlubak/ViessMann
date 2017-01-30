using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using static System.String;

namespace PolTrain.Test
{
    // Notatka------------------------------------------------------------------------------------------------------------------------
    
    // Przed uruchomieniem testów w osobnym oknie Visual Studio należy uruchomić aplikację PolTrain (drugi projekt na repozytorium)
    // Wówczas podczas działania aplikacji można rozpocząć testy (wszystkie 3 powinny wykonać się poprawnie). Nie pisałem więcej, gdyż
    // jest to po prostu duplikacja kodu i konkretnych zachowań.
    
    [TestClass]
    public class PolTrainTest
    {
        private const string Email = "adam.dlubak@gmail.com";
        private const string GoodPassword = "PolTrain!";
        private const string BadPassword = "PolTrain1";
        private IWebDriver _driver;

        [TestInitialize]
        public void TestInit()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl("http://localhost:10402/");
        }

        [TestMethod]
        public void Login_ShouldCorrectlyLogin()
        {
            LoginInput(GoodPassword);
            
            var result = _driver.FindElement(By.ClassName("item-logged")).Text;
            var expected = Email;
            Assert.IsTrue(result.Contains(expected));
        }

        [TestMethod]
        public void Login_ShouldReturnErrorLoginInformation()
        {
            LoginInput(BadPassword);

            var result = _driver.FindElement(By.Id("login-error")).Text;
            var expected = "Nieudana próba logowania!";
            Assert.IsTrue(result.Contains(expected));
        }

        [TestMethod]
        public void AddStation_ShouldCorrectlyAddStation()
        {
            Login_ShouldCorrectlyLogin();
            _driver.Navigate().GoToUrl("http://localhost:10402/Manage/ShowStations");
            var button = _driver.FindElement(By.ClassName("add-new"));
            button.Click();

            var stationCity = _driver.FindElement(By.Id("Station_City"));
            var stationName = _driver.FindElement(By.Id("Station_Name"));
            stationCity.SendKeys("Wrocław");
            stationName.SendKeys("ViessMann");
            stationName.SendKeys(Keys.Enter);

            var result = _driver.FindElement(By.ClassName("correct-changes")).Text;
            var expected = Concat("Pomyślnie wprowadzono zmiany!");

            Assert.IsTrue(result.Contains(expected));
        }

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
        }

        private void LoginInput(string password)
        {
            var emailInput = _driver.FindElement(By.Id("Email"));
            var passwordInput = _driver.FindElement(By.Id("Password"));

            emailInput.SendKeys(Email);
            passwordInput.SendKeys(password);
            passwordInput.SendKeys(Keys.Enter);

            System.Threading.Thread.Sleep(3000);
        }
    }
}
