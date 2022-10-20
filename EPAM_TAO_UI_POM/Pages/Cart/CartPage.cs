using OpenQA.Selenium;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;
using EPAM_TAO_UI_POM.Pages.Checkout;

namespace EPAM_TAO_UI_POM.Pages.Cart
{
    public class CartPage
    {
        private static readonly object syncLock = new object();
        private static CartPage _cartPage = null;

        private IWebDriver driver { get; set; }

        CartPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public static CartPage GetInstance(IWebDriver _driver)
        {
            lock (syncLock)
            {
                if (_cartPage == null)
                {
                    _cartPage = new CartPage(_driver);
                }
                return _cartPage;
            }
        }

        #region Elements/Locators        

        private By divProductPriceLocator = By.ClassName("inventory_item_price");
        private IWebElement divProductPriceElement { get; set; }

        private By btnCheckoutLocator = By.Id("checkout");
        private IWebElement btnCheckoutElement { get; set; }

        #endregion

        public string GetProductPrice()
        {
            CommonUtilities.commonUtilities.WaitForPageLoad(driver, 10);

            divProductPriceElement = CommonUtilities.commonUtilities.GetElement(driver, divProductPriceLocator);

            return divProductPriceElement.Text;
        }

        public CheckoutPage ClickOnCheckout()
        {
            btnCheckoutElement = CommonUtilities.commonUtilities.GetElement(driver, btnCheckoutLocator);
            btnCheckoutElement.Click();

            return CheckoutPage.GetInstance(driver);
        }
    }
}
