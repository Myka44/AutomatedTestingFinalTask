using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CoreLayer.WebDriver
{
    public class WebdriverWrapper
    {
        private readonly IWebDriver _driver;
        private readonly TimeSpan _timeout;

        public WebdriverWrapper(BrowserType browserType, TimeSpan timeout)
        {
            _driver = WebDriverFactory.Instance.CreateWebDriver(browserType);
            _timeout = timeout;
        }

        public void StartBrowser()
        {
            _driver.Manage().Window.Maximize();
        }

        public void CloseBrowser()
        {
            _driver.Quit();
        }

        public void NavigateToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void Click(By by)
        {
            FindElement(by, _timeout).Click();
        }

        public void EnterText(By by, string text)
        {
            var element = FindElement(by, _timeout);
            element.Clear();
            element.SendKeys(text);
        }

        public void ClearText(By by)
        {
            var element = FindElement(by, _timeout);
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
        }

        public IReadOnlyCollection<IWebElement> FindElements(By by)
        {
            FindElement(by, _timeout);
            return _driver.FindElements(by);
        }

        public IWebElement FindElement(By by)
        {
            return FindElement(by, _timeout);
        }

        //This method will thrw an exception if the element is not found in DOM or displayed otherwise it will always return a non null element
        public IWebElement FindElement(By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(_driver, timeout);
            try
            {
                return wait.Until(drv =>
                {
                    try
                    {
                        var element = drv.FindElement(by);
                        return element.Displayed ? element : null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                throw new NoSuchElementException($"Element with locator: {by} was not found or displayed within {timeout.TotalSeconds} seconds");
            }
        }
    }
}