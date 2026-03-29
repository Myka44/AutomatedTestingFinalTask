
using CoreLayer.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PageObjects
{
    public class LoginPage
    {
        private const string Url = "https://www.saucedemo.com/";
        private readonly WebdriverWrapper _driver;
        private readonly By _userNameLocator = By.CssSelector("[data-test='username']");
        private readonly By _passwordLocator = By.CssSelector("[data-test='password']");
        private readonly By _loginButtonLocator = By.CssSelector("[data-test='login-button']");
        private readonly By _errorMessageLocator = By.CssSelector("[data-test='error']");

        public LoginPage(WebdriverWrapper driver)
        {
            this._driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public LoginPage Open()
        {
            _driver.NavigateToUrl(Url);
            return this;
        }

        public MainPage Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
            return new MainPage(_driver);
        }

        public LoginPage EnterUsername(string username)
        {
            _driver.EnterText(_userNameLocator, username);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            _driver.EnterText(_passwordLocator, password);
            return this;
        }

        public LoginPage ClearUsername()
        {
            _driver.ClearText(_userNameLocator);
            return this;
        }

        public LoginPage ClearPassword()
        {
            _driver.ClearText(_passwordLocator);
            return this;
        }

        public MainPage ClickLoginButton()
        {
            _driver.Click(_loginButtonLocator);
            return new MainPage(_driver);
        }

        public string GetErrorMessage()
        {
            try
            {
                return _driver.FindElement(_errorMessageLocator).Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
        }
    }
}