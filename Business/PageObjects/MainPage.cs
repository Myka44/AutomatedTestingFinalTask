using CoreLayer.WebDriver;
using OpenQA.Selenium;

namespace Business.PageObjects
{
    public class MainPage
    {
        private readonly WebDriverWrapper _driver;
        private readonly By _burgerMenuButton = By.CssSelector("[data-test='open-menu']");
        private readonly By _swagLabsLabel = By.CssSelector(".app_logo");
        private readonly By _shoppingCartIcon = By.CssSelector("[data-test='shopping-cart-link']");
        private readonly By _sortingDropdown = By.CssSelector("[data-test='product-sort-container']");
        private readonly By _inventoryItems = By.CssSelector("[data-test='inventory-item']");

        public MainPage(WebDriverWrapper driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public bool IsBurgerMenuButtonDisplayed()
        {
            return _driver.TryFindElement(_burgerMenuButton) != null;
        }

        public bool IsSwagLabsLabelDisplayed()
        {
            return _driver.TryFindElement(_swagLabsLabel) != null;
        }

        public bool IsShoppingCartIconDisplayed()
        {
            return _driver.TryFindElement(_shoppingCartIcon) != null;
        }

        public bool IsSortingDropdownDisplayed()
        {
            return _driver.TryFindElement(_sortingDropdown) != null;
        }

        public bool IsInventoryItemsDisplayed()
        {
            return _driver.FindElements(_inventoryItems).Count > 0;
        }
    }
}