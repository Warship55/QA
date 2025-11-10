using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Threading;

namespace AutomationTestLab5
{
    [TestFixture]
    public class EbaySearchTest
    {
        private EdgeDriver driver;

        [SetUp]
        public void Setup()
        {
            // initializeaza edge
            var options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            driver = new EdgeDriver(options);
        }

        [Test]
        public void Ebay_SearchForComputer_CheckHeaderIsDisplayed()
        {
            // 1. deschide pagina ebay
            driver.Navigate().GoToUrl("https://www.ebay.com");

            // 2. Asteapta pagina sa se incarce 
            Thread.Sleep(2000);

            // 3. cauta "computer" 
            var searchBox = driver.FindElement(By.Id("gh-ac"));
            searchBox.SendKeys("computer");
            searchBox.Submit();

            // 4. asteapta rezultatele cautarii
            Thread.Sleep(3000);

            // 5. verifica dacă antetul logo ebay este afisat
            var logo = driver.FindElement(By.Id("gh-logo"));
            Assert.That(logo.Displayed, "Antet lipseste");

            // 6. afiseaza un mesaj in consola
            Console.WriteLine(" Antet ebay afisat");
        }
        [TearDown]
        public void TearDown()
        {
            // inchide browser
            driver.Quit();
        }
    }
}
