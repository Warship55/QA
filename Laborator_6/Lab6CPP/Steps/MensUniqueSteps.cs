using TechTalk.SpecFlow;
using Lab6CPP.Support;
using Lab6CPP.Pages;
using System.Collections.Generic;

namespace Lab6CPP.Steps
{
    [Binding]
    public class MensUniqueSteps
    {
        private readonly WebDriverContext _context;
        private readonly ScenarioContext _scenarioContext;
        private readonly MensPage _mensPage;

        public MensUniqueSteps(WebDriverContext context, ScenarioContext scenarioContext)
        {
            _context = context;
            _scenarioContext = scenarioContext;
            _mensPage = new MensPage(_context.Driver); // Inițializează pagina Men's Wear
        }

        // Pasul care colectează toate numele produselor afișate și le stochează pentru validări ulterioare
        [When(@"citesc toate numele produselor")]
        public void WhenCitescToateNumeleProduselor()
        {
            List<string> productNames = _mensPage.GetProductNames();
            _scenarioContext["productNames"] = productNames;
        }
    }
}
