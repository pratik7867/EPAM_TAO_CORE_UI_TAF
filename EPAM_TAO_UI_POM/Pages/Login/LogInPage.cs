using OpenQA.Selenium;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;
using EPAM_TAO_UI_POM.Pages.Products;

namespace EPAM_TAO_UI_POM.Pages.Login
{
    public class LogInPage
    {
        private static readonly object syncLock = new object();
        private static LogInPage _loginPage = null;

        private IWebDriver driver { get; set; }

        LogInPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public static LogInPage GetInstance(IWebDriver _driver)
        {
            lock (syncLock)
            {
                if (_loginPage == null)
                {
                    _loginPage = new LogInPage(_driver);
                }
                return _loginPage;
            }
        }

        #region Elements/Locators       

        private By txtUserNameLocator = By.Id("user-name");
        private IWebElement txtUserNameElement { get; set; }        

        private By txtPasswordLocator = By.Id("password");
        private IWebElement txtPasswordElement { get; set; }        

        private By btnLoginLocator = By.Id("login-button");
        private IWebElement btnLoginElement { get; set; }

        #endregion

        #region Action Methods

        public ProductsPage LogIntoApplication(string strUserName, string strPassword)
        {
            CommonUtilities.commonUtilities.WaitForPageLoad(driver, 10);

            txtUserNameElement = CommonUtilities.commonUtilities.GetElement(driver, txtUserNameLocator);
            txtUserNameElement.Clear();
            txtUserNameElement.SendKeys(strUserName);

            txtPasswordElement = CommonUtilities.commonUtilities.GetElement(driver, txtPasswordLocator);
            txtPasswordElement.Clear();
            txtPasswordElement.SendKeys(strPassword);

            btnLoginElement = CommonUtilities.commonUtilities.GetElement(driver, btnLoginLocator);
            btnLoginElement.Click();

            return ProductsPage.GetInstance(driver);
        }

        #endregion
    }
}
