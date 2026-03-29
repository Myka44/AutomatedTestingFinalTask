using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace CoreLayer.WebDriver
{
    public class WebDriverFactory
    {
        private static readonly WebDriverFactory _instance = new();
        public static WebDriverFactory Instance => _instance;

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