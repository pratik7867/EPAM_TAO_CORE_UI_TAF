using System;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace EPAM_TAO_CORE_UI_TAF.UI_Helpers
{
    internal class DriverCapabilities
    {
        private static readonly object syncLock = new object();
        private static DriverCapabilities _driverCapabilities = null;

        private string strBrowserName, strBrowserVersion;

        public enum DriverType
        {
            CHROMEDRIVER,
            FIREFOXDRIVER
        }

        DriverCapabilities()
        {

        }

        public static DriverCapabilities driverCapabilities
        {
            get
            {
                lock (syncLock)
                {
                    if (_driverCapabilities == null)
                    {
                        _driverCapabilities = new DriverCapabilities();
                    }
                    return _driverCapabilities;
                }
            }
        }

        private ICapabilities GetDriverCapabilities(IWebDriver driver)
        {
            ICapabilities browserCap = null;
            DriverType driverType = (DriverType)Enum.Parse(typeof(DriverType), driver.GetType().Name.ToUpper());

            if (driverType == DriverType.CHROMEDRIVER)
            {
                browserCap = ((ChromeDriver)driver).Capabilities;
            }
            else if (driverType == DriverType.FIREFOXDRIVER)
            {
                browserCap = ((FirefoxDriver)driver).Capabilities;
            }

            return browserCap;
        }

        public string GetBrowserName(IWebDriver driver)
        {
            try
            {
                ICapabilities capabilities = GetDriverCapabilities(driver);

                if (!string.IsNullOrEmpty(capabilities.GetCapability("browserName").ToString()))
                {
                    strBrowserName = capabilities.GetCapability("browserName").ToString();
                }
                else
                {
                    strBrowserName = "";
                }

                return strBrowserName;
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        public string GetBrowserVersion(IWebDriver driver)
        {
            try
            {
                ICapabilities capabilities = GetDriverCapabilities(driver);

                if (!string.IsNullOrEmpty(capabilities.GetCapability("browserVersion").ToString()))
                {
                    strBrowserVersion = capabilities.GetCapability("browserVersion").ToString();
                }
                else
                {
                    strBrowserVersion = "";
                }

                return strBrowserVersion;
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }
    }
}
