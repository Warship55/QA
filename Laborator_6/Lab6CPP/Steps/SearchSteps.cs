using TechTalk.SpecFlow;
using Lab6CPP.Support;
using Lab6CPP.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Lab6CPP.Steps
{
    [Binding]
    public class SearchSteps
    {
        private readonly WebDriverContext _context;
        private readonly HomePage _homePage;

        public SearchSteps(WebDriverContext context)
        {
            _context = context;
            _homePage = new HomePage(_context.Driver); // Inițializează pagina principală
        }

        // Navighează la pagina principală
        [Given(@"sunt pe pagina principală")]
        public void GivenSuntPePaginaPrincipala()
        {
            _homePage.Navigate();
        }

        // Caută produsul după textul specificat
        [When(@"caut după textul ""(.*)""")]
        public void WhenCautDupaTextul(string text)
        {
            _homePage.SearchInput.Clear();
            _homePage.SearchInput.SendKeys(text);
            _homePage.SearchSubmit.Click();

            // Pauză mică pentru a permite actualizarea paginii
            System.Threading.Thread.Sleep(1000);
        }

        // Verifică dacă rezultatele căutării conțin produsul
        [Then(@"ar trebui să văd rezultate relevante pentru produs")]
        public void ThenArTrebuieSaVadRezultate()
        {
            var bodyText = _context.Driver.FindElement(By.TagName("body")).Text;

            Assert.IsTrue(
                bodyText.Contains("Formal Blue Shirt"),
                "Nu s-au găsit rezultate pentru produsul căutat. Funcționalitatea de search poate fi neimplementată sau incompletă."
            );
        }
    }
}
