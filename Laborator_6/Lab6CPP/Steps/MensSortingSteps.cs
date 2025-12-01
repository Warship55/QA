using TechTalk.SpecFlow;
using Lab6CPP.Support;
using Lab6CPP.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Lab6CPP.Steps
{
    [Binding]
    public class MensSortingSteps
    {
        private readonly WebDriverContext _context;
        private readonly ScenarioContext _scenarioContext;
        private readonly MensPage _mensPage;

        public MensSortingSteps(WebDriverContext context, ScenarioContext scenarioContext)
        {
            _context = context;
            _scenarioContext = scenarioContext;
            _mensPage = new MensPage(_context.Driver); // Inițializează pagina Men's Wear
        }

        // Pasul pentru navigarea pe pagina Men's Wear
        [Given(@"sunt pe pagina Men's wear")]
        public void GivenSuntPePaginaMensWear()
        {
            _mensPage.Navigate();
        }

        // Pasul care sortează produsele după criteriul selectat (ex: Name(A - Z))
        [When(@"sortez produsele după ""(.*)""")]
        public void WhenSortezProduseleDupa(string criteriu)
        {
            var selectElement = new SelectElement(_mensPage.SortBySelect);
            selectElement.SelectByText(criteriu); // Selectează criteriul din dropdown

            // Pauză scurtă pentru ca JavaScript-ul să aplice sortarea
            System.Threading.Thread.Sleep(1000);

            // Preluăm lista de nume după sortare și o stocăm în ScenarioContext
            List<string> names = _mensPage.GetProductNames();
            _scenarioContext["productNames"] = names;
        }
    }
}
