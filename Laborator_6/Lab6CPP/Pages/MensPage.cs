using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Lab6CPP.Pages
{
    public class MensPage
    {
        private readonly IWebDriver _driver;
        private const string MensUrl = "https://adoring-pasteur-3ae17d.netlify.app/mens";

        public MensPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl(MensUrl);
        }

        // dropdown Sort By
        public IWebElement SortBySelect =>
            _driver.FindElement(By.Id("country1"));

        // toate produsele (cardurile)
        public IReadOnlyCollection<IWebElement> ProductCards =>
            _driver.FindElements(By.CssSelector(".men-pro-item"));

        public List<string> GetProductNames()
        {
            return ProductCards
                .Select(card => card.FindElement(By.CssSelector(".item-info-product h4 a")).Text.Trim())
                .ToList();
        }

        public List<decimal> GetProductPrices()
        {
            return ProductCards
                .Select(card =>
                {
                    var priceText = card.FindElement(By.CssSelector(".info-product-price .item_price")).Text.Trim();
                    // ex: "$45.99" -> 45.99
                    return decimal.Parse(priceText.Replace("$", ""), System.Globalization.CultureInfo.InvariantCulture);
                })
                .ToList();
        }

        // slider Range
        public IWebElement PriceSlider =>
            _driver.FindElement(By.Id("slider-range"));
    }
}
