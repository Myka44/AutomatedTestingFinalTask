using CoreLayer.WebDriver;
using OpenQA.Selenium;

namespace Business.PageObjects
{
    public class MainPage
    {
        private readonly WebdriverWrapper _driver;
        private readonly By _burgerMenuButton = By.CssSelector("[data-test='open-menu']");
        private readonly By _swagLabsLabel = By.CssSelector(".app_logo");
        private readonly By _shoppingCartIcon = By.CssSelector("[data-test='shopping-cart-link']");
        private readonly By _sortingDropdown = By.CssSelector("[data-test='product-sort-container']");
        private readonly By _inventoryItems = By.CssSelector("[data-test='inventory-item']");

        public MainPage(WebdriverWrapper driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public bool IsBurgerMenuButtonDisplayed()
        {
            _driver.FindElement(_burgerMenuButton);
            return true;
        }

        public bool IsSwagLabsLabelDisplayed()
        {
            _driver.FindElement(_swagLabsLabel);
            return true;
        }

        public bool IsShoppingCartIconDisplayed()
        {
            _driver.FindElement(_shoppingCartIcon);
            return true;
        }

        public bool IsSortingDropdownDisplayed()
        {
            _driver.FindElement(_sortingDropdown);
            return true;
        }

        public bool IsInventoryItemsDisplayed()
        {
            return _driver.FindElements(_inventoryItems).Count > 0;

        }
    }
}