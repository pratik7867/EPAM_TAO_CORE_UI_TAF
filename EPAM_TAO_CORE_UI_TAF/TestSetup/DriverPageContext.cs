using System;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;

namespace EPAM_TAO_CORE_UI_TAF.TestSetup
{
    public abstract class DriverPageContext
    {
        public DriverPageContext(IWebDriver driver)
        {
            try
            {
                PageFactory.InitElements(driver, this);
            }
            catch(Exception ex)
            {                
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);            
                throw ex;
            }
        }
    }
}
