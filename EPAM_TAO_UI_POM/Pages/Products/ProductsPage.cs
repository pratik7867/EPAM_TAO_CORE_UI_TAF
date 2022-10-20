using OpenQA.Selenium;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;
using EPAM_TAO_UI_POM.Pages.Cart;

namespace EPAM_TAO_UI_POM.Pages.Products
{
    public class ProductsPage
    {
        private static readonly object syncLock = new object();
        private static ProductsPage _productsPage = null;

        private IWebDriver driver { get; set; }

        ProductsPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public static ProductsPage GetInstance(IWebDriver _driver)
        {
            lock (syncLock)
            {
                if (_productsPage == null)
                {
                    _productsPage = new ProductsPage(_driver);
                }
                return _productsPage;
            }
        }

        #region Elements/Locators

        private string strDivProductPriceLocatorValue = ".//button[@id='add-to-cart-{0}']/preceding::div[@class='inventory_item_price']";
        private By divProductPriceLocator { get; set; }
        private IWebElement divProductPriceElement { get; set; }

        private string strBtnAddToCartLocatorValue = "add-to-cart-{0}";
        private By btnAddToCartLocator { get; set; }
        private IWebElement btnAddToCartElement { get; set; }

        private By btnShoppingCartLocator = By.Id("shopping_cart_container");
        private IWebElement btnShoppingCartElement { get; set; }

        #endregion

        #region Action Methods

        public string getProductPrice(string strProductName)
        {
            CommonUtilities.commonUtilities.WaitForPageLoad(driver, 10);            

            divProductPriceLocator = By.XPath(CommonUtilities.commonUtilities.GetDynamicLocatorString(strDivProductPriceLocatorValue, strProductName, " ", "-").ToLower());
            divProductPriceElement = CommonUtilities.commonUtilities.WaitForElementToBeVisible(driver, divProductPriceLocator, 5);

            return divProductPriceElement.Text;
        }

        public void AddToCart(string strProductName)
        {
            btnAddToCartLocator = By.Id(CommonUtilities.commonUtilities.GetDynamicLocatorString(strBtnAddToCartLocatorValue, strProductName, " ", "-").ToLower());
            btnAddToCartElement = CommonUtilities.commonUtilities.WaitForElementToBeVisible(driver, btnAddToCartLocator, 5);

            btnAddToCartElement.Click();            
        }

        public CartPage ClickOnShoppingCart()
        {
            btnShoppingCartElement = CommonUtilities.commonUtilities.GetElement(driver, btnShoppingCartLocator);
            btnShoppingCartElement.Click();

            return CartPage.GetInstance(driver);
        }

        #endregion
    }
}
