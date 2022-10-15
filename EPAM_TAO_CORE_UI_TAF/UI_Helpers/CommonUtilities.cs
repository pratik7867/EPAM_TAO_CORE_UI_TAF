using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EPAM_TAO_CORE_UI_TAF.UI_Helpers
{
    public class CommonUtilities
    {
        private static readonly object syncLock = new object();
        private static CommonUtilities _commonUtilities = null;

        private static Actions actions;

        private static string strParentDir, strSSDir, strPathToSSFile;

        CommonUtilities()
        {

        }

        public static CommonUtilities commonUtilities
        {
            get
            {
                lock(syncLock)
                {
                    if(_commonUtilities == null)
                    {
                        _commonUtilities = new CommonUtilities();
                    }
                    return _commonUtilities;
                }
            }
        }        

        #region Navigation Utils

        public void NavigateToURL(IWebDriver driver, string strURL)
        {
            try
            {
                driver.Navigate().GoToUrl(strURL);
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        public void MaximizeWindow(IWebDriver driver)
        {
            try
            {
                driver.Manage().Window.Maximize();
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        #endregion

        #region Find Element(s) Utils

        public IWebElement GetElement(IWebDriver driver, By locator)
        {
            try
            {
                return driver.FindElement(locator);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        public IList<IWebElement> GetElements(IWebDriver driver, By locator)
        {
            try
            {
                return driver.FindElements(locator);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        #endregion

        #region Wait Utils

        public void WaitForPageLoad(IWebDriver driver, int intWaitForNoOfSeconds)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(intWaitForNoOfSeconds));
                wait.Until(_driver => ExecuteJS(driver, "return document.readyState").Equals("complete"));
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        public void WaitForAjaxToComplete(IWebDriver driver, int intWaitForNoOfSeconds)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(intWaitForNoOfSeconds));
                wait.Until(_driver => ExecuteJS(driver, "return window.jQuery && window.jQuery.active == 0"));
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        public IWebElement WaitForElementToBeVisible(IWebDriver driver, By locator, int intWaitForNoOfSeconds)
        {
            try
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(intWaitForNoOfSeconds)).Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        public IList<IWebElement> WaitForElementsCollectionToBeVisible(IWebDriver driver, By locator, int intWaitForNoOfSeconds)
        {
            try
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(intWaitForNoOfSeconds)).Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        public IWebElement WaitForElementToBeClickable(IWebDriver driver, By locator, int intWaitForNoOfSeconds)
        {
            try
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(intWaitForNoOfSeconds)).Until(ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        #endregion

        #region IFrame Utils        

        public void WaitForIFrameAndSwitchToIt(IWebDriver driver, By locator, int intWaitForNoOfSeconds)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(intWaitForNoOfSeconds)).Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(locator));
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        public void WaitForIFrameAndSwitchToIt(IWebDriver driver, string strFrame, int intWaitForNoOfSeconds)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(intWaitForNoOfSeconds)).Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(strFrame));
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        public void SwitchToParentFrame(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().ParentFrame();
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        public void SwitchToDefaultContent(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }


        #endregion

        #region Drop-Down Utils

        public void SelectOptionByIndex(SelectElement selectElement, int intIndex)
        {
            try
            {
                selectElement.SelectByIndex(intIndex);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public void SelectOptionByValue(SelectElement selectElement, string strValue)
        {
            try
            {
                selectElement.SelectByValue(strValue);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public void SelectOptionByText(SelectElement selectElement, string strText, bool boolIsPartialMatch = false)
        {
            try
            {
                selectElement.SelectByText(strText, boolIsPartialMatch);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        #endregion

        #region JS Util

        public object ExecuteJS(IWebDriver driver, string strJS, IWebElement element = null)
        {
            try
            {
                if (element == null)
                {
                    return ((IJavaScriptExecutor)driver).ExecuteScript(strJS);
                }
                else
                {
                    return ((IJavaScriptExecutor)driver).ExecuteScript(strJS, element);
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        #endregion

        #region Action Utils

        public void MoveToElementAndClick(IWebDriver driver, IWebElement element)
        {
            try
            {
                actions = new Actions(driver);
                actions.MoveToElement(element).Click().Build().Perform();
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        public void DoubleClick(IWebDriver driver)
        {
            try
            {
                actions = new Actions(driver);
                actions.DoubleClick().Perform();
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        #region Additonal Utils

        public string TakeScreenshot(IWebDriver driver, string strSSFileName)
        {
            try
            {                
                strParentDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DateTime.Now.ToString("dd-MM-yyyy"));
                strSSDir = strParentDir + @"\" + "Screenshots";

                lock(syncLock)
                {
                    if(!Directory.Exists(strParentDir))
                    {
                        Directory.CreateDirectory(strParentDir);
                    }
                    else
                    {
                        if(!Directory.Exists(strSSDir))
                        {
                            Directory.CreateDirectory(strSSDir);
                        }
                    }
                }

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                Guid guid = Guid.NewGuid();
                strPathToSSFile = strSSDir + @"\" + strSSFileName + "_" + guid;
                screenshot.SaveAsFile(strPathToSSFile, ScreenshotImageFormat.Png);

                return strPathToSSFile;
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }

        }

        public string GetPageTitle(IWebDriver driver)
        {
            try
            {
                return driver.Title;
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                throw ex;
            }
        }

        #endregion

        #endregion

    }
}
