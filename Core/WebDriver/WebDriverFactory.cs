using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace CoreLayer.WebDriver
{
    public class WebDriverFactory
    {
        public static WebDriverFactory Instance => new WebDriverFactory();
        private WebDriverFactory(){}

        public IWebDriver CreateWebDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return new ChromeDriver();
                case BrowserType.Edge:
                    return new EdgeDriver();
                default:
                    throw new ArgumentException($"Unsupported browser type: {browserType}");
            }
        }
    }
}