using Lab6CPP.Support;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace MyProject.Steps
{
    [Binding]
    public class HomePageSteps
    {
        private readonly IWebDriver _driver;

        public HomePageSteps(WebDriverContext context)
        {
            _driver = context.Driver;
        }

        [Given(@"utilizatorul deschide aplicația")]
        public void GivenUtilizatorulDeschideAplicatia()
        {
            _driver.Navigate().GoToUrl("https://adoring-pasteur-3ae17d.netlify.app/");
        }

        [Then(@"pagina principală este afișată corect")]
        public void ThenPaginaPrincipalaEsteAfisataCorect()
        {
            Assert.IsTrue(
                _driver.Title.Contains("Home") ||
                _driver.PageSource.Contains("Featured"),
                "Pagina principală nu este afișată corect."
            );
        }
    }
}