using OpenQA.Selenium;

namespace Lab6CPP.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private const string HomeUrl = "https://adoring-pasteur-3ae17d.netlify.app/";

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl(HomeUrl);
        }

        public IWebElement SearchInput =>
            _driver.FindElement(By.Name("search"));

        public IWebElement SearchSubmit =>
            _driver.FindElement(By.CssSelector(".header-middle form input[type='submit']"));
    }
}
