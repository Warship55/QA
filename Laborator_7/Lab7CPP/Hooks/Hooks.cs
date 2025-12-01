using System;
using System.IO;
using System.Threading;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Lab7CPP.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _container;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");

            // Setarea unui user-agent custom
            string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                               "AppleWebKit/537.36 (KHTML, like Gecko) " +
                               "Chrome/142.0.7444.175 Safari/537.36";
            options.AddArgument($"--user-agent={userAgent}");

            // Opțiuni suplimentare pentru a evita detectarea Selenium
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);

            // Inițializarea driver-ului Chrome
            IWebDriver driver = new ChromeDriver(options);

            // Înregistrarea driver-ului în container pentru SpecFlow
            _container.RegisterInstanceAs(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();

            try
            {
                // Dacă scenariul a eșuat, salvăm screenshot
                if (_scenarioContext.TestError != null)
                {
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    var fileName = $"{SanitizeFileName(_scenarioContext.ScenarioInfo.Title)}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                    Directory.CreateDirectory(path);

                    var fullPath = Path.Combine(path, fileName);
                    File.WriteAllBytes(fullPath, screenshot.AsByteArray);
                }

                // Pauză pentru a putea rezolva manual eventuale CAPTCHA-uri sau pentru vizualizare
                Thread.Sleep(5000); // 5 secunde
            }
            finally
            {
                driver.Quit();
            }
        }

        // Funcție pentru a curăța numele fișierelor
        private static string SanitizeFileName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name;
        }
    }
}
