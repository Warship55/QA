using OpenQA.Selenium;

namespace Lab6CPP.Pages
{
    public class SingleProductPage
    {
        private readonly IWebDriver _driver;
        private const string SingleUrl = "https://adoring-pasteur-3ae17d.netlify.app/single";

        public SingleProductPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl(SingleUrl);
        }

        public IWebElement NameInput =>
            _driver.FindElement(By.CssSelector(".add-review input[name='Name']"));

        public IWebElement EmailInput =>
            _driver.FindElement(By.CssSelector(".add-review input[name='Email']"));

        public IWebElement MessageTextarea =>
            _driver.FindElement(By.CssSelector(".add-review textarea[name='Message']"));

        public IWebElement SendButton =>
            _driver.FindElement(By.CssSelector(".add-review input[type='submit']"));
    }
}
