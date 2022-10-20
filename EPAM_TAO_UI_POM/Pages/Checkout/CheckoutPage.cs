using OpenQA.Selenium;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;

namespace EPAM_TAO_UI_POM.Pages.Checkout
{
    public class CheckoutPage
    {
        private static readonly object syncLock = new object();
        private static CheckoutPage _checkoutPage = null;

        private IWebDriver driver { get; set; }

        CheckoutPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public static CheckoutPage GetInstance(IWebDriver _driver)
        {
            lock (syncLock)
            {
                if (_checkoutPage == null)
                {
                    _checkoutPage = new CheckoutPage(_driver);
                }
                return _checkoutPage;
            }
        }

        #region Elements/Locators

        private By txtFirstNameLocator = By.Id("first-name");
        private IWebElement txtFirstNameElement { get; set; }        

        private By txtLastNameLocator = By.Id("last-name");
        private IWebElement txtLastNameElement { get; set; }        

        private By txtPostalCodeLocator = By.Id("postal-code");
        private IWebElement txtPostalCodeElement { get; set; }        

        private By btnContinueLocator = By.Id("continue");
        private IWebElement btnContinueElement { get; set; }        

        private By divProductNameLocator = By.ClassName("inventory_item_name");
        private IWebElement divProductNameElement { get; set; }

        private By divProductPriceLocator = By.ClassName("inventory_item_price");
        private IWebElement divProductPriceElement { get; set; }                

        private By btnFinishLocator = By.Id("finish");
        private IWebElement btnFinishElement { get; set; }

        #endregion

        #region Action Mehods

        public void FillUpCheckoutDetailsAndContiue(string strFirstName, string strLastName, string strPostalCode)
        {
            CommonUtilities.commonUtilities.WaitForPageLoad(driver, 10);

            txtFirstNameElement = CommonUtilities.commonUtilities.GetElement(driver, txtFirstNameLocator);
            txtFirstNameElement.Clear();
            txtFirstNameElement.SendKeys(strFirstName);

            txtLastNameElement = CommonUtilities.commonUtilities.GetElement(driver, txtLastNameLocator);
            txtLastNameElement.Clear();
            txtLastNameElement.SendKeys(strLastName);

            txtPostalCodeElement = CommonUtilities.commonUtilities.GetElement(driver, txtPostalCodeLocator);
            txtPostalCodeElement.Clear();
            txtPostalCodeElement.SendKeys(strPostalCode);

            btnContinueElement = CommonUtilities.commonUtilities.GetElement(driver, btnContinueLocator);
            btnContinueElement.Click();
        }

        public string GetProductName()
        {
            divProductNameElement = CommonUtilities.commonUtilities.GetElement(driver, divProductNameLocator);

            return divProductNameElement.Text;
        }

        public string GetProductPrice()
        {
            divProductPriceElement = CommonUtilities.commonUtilities.GetElement(driver, divProductPriceLocator);

            return divProductPriceElement.Text;
        }

        public void ClickOnFinish()
        {
            btnFinishElement = CommonUtilities.commonUtilities.GetElement(driver, btnFinishLocator);
            btnFinishElement.Click();
        }

        #endregion
    }
}
