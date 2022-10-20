using TechTalk.SpecFlow;
using OpenQA.Selenium;
using FluentAssertions;
using EPAM_TAO_BDD_UI_TESTS.UITests.Steps.Common;
using EPAM_TAO_BDD_UI_TESTS.Hooks;
using EPAM_TAO_UI_POM.Pages.Login;

namespace EPAM_TAO_BDD_UI_TESTS.UITests.Steps.Checkout
{
    [Binding]
    public sealed class CheckoutStepDefinition : CommonStepDefinition
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef        

        public CheckoutStepDefinition(ScenarioContext scenarioContext) : base(scenarioContext)
        {            
        }

        [Given(@"the user is logged in with valid credentials ""(.*)"", ""(.*)""")]
        public void GivenTheUserIsLoggedInWithValidCredentials(string strUserName, string strPassword)
        {
            productsPage = LogInPage.GetInstance(TestHooks.objectContainer.Resolve<IWebDriver>()).LogIntoApplication(strUserName, strPassword);
        }

        [When(@"the user adds product to the cart ""(.*)""")]
        public void WhenTheUserAddsProductToTheCart(string strProductName)
        {
            scenarioContext.Add("ProductPrice", productsPage.getProductPrice(strProductName));
            productsPage.AddToCart(strProductName);
        }

        [When(@"the user navigates to cart page")]
        public void WhenTheUserNavigatesToCartPage()
        {
            cartPage = productsPage.ClickOnShoppingCart();
        }

        [Then(@"the product should be added to the cart with product price")]
        public void ThenTheProductShouldBeAddedToTheCartWithProductPrice()
        {
            var actualProductPrice = cartPage.GetProductPrice();
            var expectedProductPrice = scenarioContext.Get<string>("ProductPrice");

            actualProductPrice.Should().BeEquivalentTo(expectedProductPrice);
        }

        [When(@"the user navigates to checkout page")]
        public void WhenTheUserNavigatesToCheckoutPage()
        {
            checkoutPage = cartPage.ClickOnCheckout();
        }

        [When(@"the user fills up checkout details ""(.*)"", ""(.*)"", ""(.*)"" and click on continue")]
        public void WhenTheUserFillsUpCheckoutDetailsAndClickOnContinue(string strFirstName, string strLastName, string strPostalCode)
        {
            checkoutPage.FillUpCheckoutDetailsAndContiue(strFirstName, strLastName, strPostalCode);
        }

        [Then(@"the product should be checkedout with product name, product price ""(.*)"", ""(.*)""")]
        public void ThenTheProductShouldBeCheckedoutWithProductNameAndProductPrice(string strProductName, string strProductPrice)
        {
            var actualProductName = checkoutPage.GetProductName();
            var actualProductPrice = checkoutPage.GetProductPrice();

            var expectedProductName = strProductName;
            var expectedProductPrice = strProductPrice;

            actualProductName.Should().BeEquivalentTo(expectedProductName);
            actualProductPrice.Should().BeEquivalentTo(expectedProductPrice);
        }
    }
}
