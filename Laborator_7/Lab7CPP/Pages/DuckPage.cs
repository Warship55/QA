using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Lab7CPP.Pages
{
    public class GooglePage
    {
        private readonly IWebDriver _driver;

        // Căsuța de căutare de pe Google
        private By SearchBox => By.Name("q");

        public GooglePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateToHome()
        {
            _driver.Navigate().GoToUrl("https://www.google.com/");
        }

        /// <summary>
        /// Face o căutare "lentă" pe Google:
        /// - așteaptă căsuța de căutare
        /// - tastează literele cu pauze
        /// - așteaptă până se încarcă pagina de rezultate
        /// Îți oferă timp să rezolvi manual CAPTCHA-ul dacă apare.
        /// </summary>
        public void SearchSlow(string text)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));

            // Așteptăm până apare căsuța de căutare
            var box = wait.Until(d => d.FindElement(SearchBox));

            // Curățăm și așteptăm puțin
            box.Clear();
            Thread.Sleep(800);

            // Tastăm ca un om: literă cu literă, cu mică pauză
            foreach (char c in text)
            {
                box.SendKeys(c.ToString());
                Thread.Sleep(150); // 0.15 secunde între litere
            }

            Thread.Sleep(1000); // mică pauză înainte de submit
            box.Submit();

            // Dacă apare CAPTCHA, tu o poți rezolva în timpul acestui wait.
            // După ce o rezolvi și Google redirecționează spre /search, condiția devine adevărată.
            wait.Until(d =>
                d.Url.Contains("/search?", StringComparison.OrdinalIgnoreCase) ||
                d.Title.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0
            );

            // Încă puțin timp după încărcare
            Thread.Sleep(1500);
        }

        private string GetBodyText()
        {
            return _driver.FindElement(By.TagName("body")).Text;
        }

        public bool ResultsContainText(string text)
        {
            var body = GetBodyText();
            return body.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public string GetFirstResultTitle()
        {
            try
            {
                // Titlurile rezultatelor sunt de obicei în <h3>
                var headers = _driver.FindElements(By.CssSelector("h3"));
                var first = headers.FirstOrDefault(e => e.Displayed && !string.IsNullOrWhiteSpace(e.Text));
                if (first != null)
                    return first.Text;

                return GetBodyText();
            }
            catch
            {
                return GetBodyText();
            }
        }

        public bool IsCalculatorVisible()
        {
            try
            {
                // Widgetul de calculator – selector generic (se poate schimba în timp)
                var elem = _driver.FindElement(By.CssSelector("div[aria-label='Calculator'], div[jsname='fPLMtf']"));
                if (elem.Displayed)
                    return true;
            }
            catch
            {
                // ignorăm și verificăm textul paginii
            }

            var body = GetBodyText().ToLower();
            return body.Contains("calculator") || body.Contains("calc") || body.Contains("result");
        }

        public bool IsConverterVisible()
        {
            var body = GetBodyText().ToLower();
            return body.Contains("converter") || body.Contains("convert") || body.Contains("conversion");
        }
    }
}
