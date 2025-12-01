using Lab7CPP.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Lab7CPP.Steps
{
    [Binding]
    public class GoogleVariant3Steps
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly GooglePage _googlePage;

        private string _firstResultTitle = string.Empty;

        public GoogleVariant3Steps(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
            _googlePage = new GooglePage(_driver);
        }

        [Given(@"că sunt pe pagina principală Google")]
        public void GivenCaSuntPePaginaPrincipalaGoogle()
        {
            _googlePage.NavigateToHome();
        }

        [When(@"caut pe Google ""(.*)""")]
        public void WhenCautPeGoogle(string text)
        {
            _googlePage.SearchSlow(text);
        }

        [Then(@"rezultatele Google trebuie să conțină ""(.*)""")]
        public void ThenRezultateleGoogleTrebuieSaContina(string text)
        {
            Assert.IsTrue(
                _googlePage.ResultsContainText(text),
                $"Rezultatele Google nu conțin textul: {text}"
            );
        }

        [When(@"memorez primul rezultat Google")]
        public void WhenMemorezPrimulRezultatGoogle()
        {
            _firstResultTitle = _googlePage.GetFirstResultTitle();
            Assert.IsFalse(
                string.IsNullOrWhiteSpace(_firstResultTitle),
                "Nu am găsit primul rezultat Google (titlu gol)."
            );
        }

        [Then(@"primul rezultat Google trebuie să fie similar pentru case-insensitive")]
        public void ThenPrimulRezultatGoogleTrebuieSaFieSimilarPentruCase_Insensitive()
        {
            var second = _googlePage.GetFirstResultTitle();
            Assert.IsFalse(
                string.IsNullOrWhiteSpace(second),
                "Al doilea rezultat Google (pentru 'google') e gol."
            );

            Assert.AreEqual(
                _firstResultTitle.ToLower(),
                second.ToLower(),
                "Primul rezultat diferă prea mult între 'Google' și 'google'."
            );
        }

        [Then(@"serviciul de calculator Google trebuie să fie afișat")]
        public void ThenServiciulDeCalculatorGoogleTrebuieSaFieAfisat()
        {
            Assert.IsTrue(
                _googlePage.IsCalculatorVisible(),
                "Serviciul/Widgetul de calculator Google nu pare să fie afișat."
            );
        }

        [Then(@"serviciul de conversie Google trebuie să fie afișat în partea de sus")]
        public void ThenServiciulDeConversieGoogleTrebuieSaFieAfisatInParteaDeSus()
        {
            Assert.IsTrue(
                _googlePage.IsConverterVisible(),
                "Serviciul de conversie nu pare să fie afișat în rezultate."
            );
        }
    }
}
