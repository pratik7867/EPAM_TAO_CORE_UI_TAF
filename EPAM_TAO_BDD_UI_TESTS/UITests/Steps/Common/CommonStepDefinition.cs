using TechTalk.SpecFlow;
using EPAM_TAO_UI_POM.Pages.Products;
using EPAM_TAO_UI_POM.Pages.Cart;
using EPAM_TAO_UI_POM.Pages.Checkout;

namespace EPAM_TAO_BDD_UI_TESTS.UITests.Steps.Common
{
    [Binding]
    public class CommonStepDefinition
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        protected readonly ScenarioContext scenarioContext;
        
        protected ProductsPage productsPage { get; set; }
        protected CartPage cartPage { get; set; }
        protected CheckoutPage checkoutPage { get; set; }

        public CommonStepDefinition(ScenarioContext _scenarioContext)
        {
            scenarioContext = _scenarioContext;            
        }
    }
}
