using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Lab6CPP.Support
{
    [Binding]
    public class Hooks
    {
        private readonly WebDriverContext _context;

        public Hooks(WebDriverContext context)
        {
            _context = context;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var options = new ChromeOptions();
            // opțional: options.AddArgument("start-maximized");

            _context.Driver = new ChromeDriver(options); // ChromeDriver din NuGet, fără cale absolută
            _context.Driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _context.Driver?.Quit();
        }
    }
}
