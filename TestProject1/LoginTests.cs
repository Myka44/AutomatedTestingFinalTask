using Business.PageObjects;
using CoreLayer.WebDriver;
using FluentAssertions;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Edge)]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class LoginTests
    {
        private WebdriverWrapper _driverWrapper;
        private readonly BrowserType _browserType;
        private static readonly TimeSpan TestTimeout = TimeSpan.FromSeconds(15);

        public LoginTests(BrowserType browserType)
        {
            _browserType = browserType;
        }

        [SetUp]
        public void SetUp()
        {
            TestContext.Out.WriteLine($"SetUp: browser: {_browserType} for test: {TestContext.CurrentContext.Test.Name}");
            _driverWrapper = new WebdriverWrapper(_browserType, TestTimeout);
            _driverWrapper.StartBrowser();
        }

        [TearDown]
        public void TearDown()
        {
            TestContext.Out.WriteLine($"TearDown: closing {_browserType} after test: {TestContext.CurrentContext.Test.Name}");
            _driverWrapper.CloseBrowser();
        }

        [Test]
        public void TestLoginWithEmptyCredentials()
        {
            //Given
            TestContext.Out.WriteLine($"Test: TestLoginWithEmptyCredentials on {_browserType}");
            var loginPage = new LoginPage(_driverWrapper);
            loginPage.Open();

            //When
            loginPage.EnterUsername("abc");
            loginPage.EnterPassword("123");
            loginPage.ClearUsername();
            loginPage.ClearPassword();
            loginPage.ClickLoginButton();

            //Then
            var errorMessage = loginPage.GetErrorMessage();
            TestContext.Out.WriteLine($"Result: error message: '{errorMessage}'");
            errorMessage.Should().Be("Epic sadface: Username is required");
        }

        [Test]
        public void TestLoginWithOnlyUsername()
        {
            //Given
            TestContext.Out.WriteLine($"Test: TestLoginWithOnlyUsername on {_browserType}");
            var loginPage = new LoginPage(_driverWrapper);
            loginPage.Open();

            //When
            loginPage.EnterUsername("abc");
            loginPage.EnterPassword("123");
            loginPage.ClearPassword();
            loginPage.ClickLoginButton();

            //Then
            var errorMessage = loginPage.GetErrorMessage();
            TestContext.Out.WriteLine($"Result: error message: '{errorMessage}'");
            errorMessage.Should().Be("Epic sadface: Password is required");
        }

        [TestCase("standard_user", "secret_sauce")]
        [TestCase("performance_glitch_user", "secret_sauce")]
        [TestCase("problem_user", "secret_sauce")]
        public void TestLoginWithValidCredentials(string username, string password)
        {
            //Given
            TestContext.Out.WriteLine($"Test: TestLoginWithValidCredentials on {_browserType}, username: {username}, password {password}");
            var loginPage = new LoginPage(_driverWrapper);
            loginPage.Open();

            //When
            var mainPage = loginPage.Login(username, password);

            //Then
            mainPage.IsBurgerMenuButtonDisplayed().Should().BeTrue("burger menu button should be visible after login");
            mainPage.IsSwagLabsLabelDisplayed().Should().BeTrue("Swag Labs label should be visible after login");
            mainPage.IsShoppingCartIconDisplayed().Should().BeTrue("shopping cart icon should be visible after login");
            mainPage.IsSortingDropdownDisplayed().Should().BeTrue("sorting dropdown should be visible after login");
            mainPage.IsInventoryItemsDisplayed().Should().BeTrue("inventory items should be visible after login");
        }
    }
}