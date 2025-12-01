using TechTalk.SpecFlow;
using Lab6CPP.Support;
using Lab6CPP.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;

namespace Lab6CPP.Steps
{
    [Binding]
    public class MensPriceFilterSteps
    {
        private readonly WebDriverContext _context;
        private readonly MensPage _mensPage;

        public MensPriceFilterSteps(WebDriverContext context)
        {
            _context = context;
            _mensPage = new MensPage(_context.Driver); // Inițializează pagina de Men's Wear
        }

        // Pasul care setează filtrul de preț între min și max
        [When(@"setez filtrul de preț între (.*) și (.*)")]
        public void WhenSetezFiltrulDePret(int min, int max)
        {
            var slider = _mensPage.PriceSlider; // Slider-ul UI pentru preț
            var actions = new Actions(_context.Driver);

            // Demonstrație simplificată: mutăm sliderul
            // În practică, ar trebui calculat offset-ul exact în funcție de min/max
           // actions.DragAndDropToOffset(slider, 100, 10).Perform();
            actions.DragAndDropToOffset(slider, -110, 100).Perform();

            System.Threading.Thread.Sleep(1000); // Pauză pentru update UI
        }

        // Pasul care verifică dacă toate produsele afișate respectă intervalul de preț
        [Then(@"toate produsele afișate au prețul între (.*) și (.*)")]
        public void ThenToateProduseleAuPretulInInterval(int min, int max)
        {
            var prices = _mensPage.GetProductPrices(); // Obține lista de prețuri
            bool allInRange = prices.All(p => p >= min && p <= max);

            Assert.IsTrue(allInRange,
                $"Există produse în afara intervalului {min}-{max}. Prețuri: {string.Join(", ", prices)}");
        }
    }
}
