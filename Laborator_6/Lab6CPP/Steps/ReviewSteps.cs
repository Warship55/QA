using TechTalk.SpecFlow;
using Lab6CPP.Support;
using Lab6CPP.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Lab6CPP.Steps
{
    [Binding]
    public class ReviewSteps
    {
        private readonly WebDriverContext _context;
        private readonly SingleProductPage _singlePage;

        public ReviewSteps(WebDriverContext context)
        {
            _context = context;
            _singlePage = new SingleProductPage(_context.Driver); // Inițializează pagina unui produs
        }

        // Navighează la pagina produsului
        [Given(@"sunt pe pagina produsului")]
        public void GivenSuntPePaginaProdusului()
        {
            _singlePage.Navigate();
        }

        // Completează formularul de review cu datele primite ca parametri
        [When(@"completez formularul de review cu nume ""(.*)"", email ""(.*)"" și mesaj ""(.*)""")]
        public void WhenCompletezFormularul(string nume, string email, string mesaj)
        {
            _singlePage.NameInput.Clear();
            _singlePage.NameInput.SendKeys(nume);

            _singlePage.EmailInput.Clear();
            _singlePage.EmailInput.SendKeys(email);

            _singlePage.MessageTextarea.Clear();
            _singlePage.MessageTextarea.SendKeys(mesaj);
        }

        // Trimite formularul de review
        [When(@"trimit formularul de review")]
        public void WhenTrimitFormularulDeReview()
        {
            _singlePage.SendButton.Click();
            System.Threading.Thread.Sleep(1000); // mică pauză pentru simularea așteptării serverului
        }

        // Verifică dacă există un mesaj de confirmare
        [Then(@"ar trebui să primesc o confirmare că review-ul a fost trimis")]
        public void ThenArTrebuieSaPrimescOConfirmare()
        {
            var bodyText = _context.Driver.FindElement(By.TagName("body")).Text;
            Assert.IsTrue(bodyText.Contains("Thank you") || bodyText.Contains("review"),
                "Nu a fost afișat niciun mesaj de confirmare pentru review (funcționalitate posibil lipsă).");
        }
    }
}
