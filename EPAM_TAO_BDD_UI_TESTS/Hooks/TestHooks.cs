using System;
using System.Reflection;
using System.Configuration;
using TechTalk.SpecFlow;
using BoDi;
using EPAM_TAO_CORE_UI_TAF.TestSetup;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;

namespace EPAM_TAO_BDD_UI_TESTS.Hooks
{   
    [Binding]
    public sealed class TestHooks : TestHookup
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        
        private string strBrowser { get { return ConfigurationManager.AppSettings["Browser"].ToString(); } }
        private string strSiteURL { get { return ConfigurationManager.AppSettings["SiteURL"].ToString(); } }
        private string strAUT { get { return ConfigurationManager.AppSettings["AUT"].ToString(); } }

        public static readonly IObjectContainer objectContainer = new ObjectContainer();

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            //TODO: implement logic that has to run before executing each scenario
            try
            {
                driver = InitBrowser((BrowserType)Enum.Parse(typeof(BrowserType), strBrowser.ToUpper()));
                objectContainer.RegisterInstanceAs(driver);

                CommonUtilities.commonUtilities.NavigateToURL(driver, strSiteURL);
                CommonUtilities.commonUtilities.MaximizeWindow(driver);

                ExtentReportHelper.GetInstance(strAUT, driver).CreateTest(scenarioContext.ScenarioInfo.Title);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                ExtentReportHelper.GetInstance(strAUT, driver).SetTestStatusFail($"<br>{ex.Message}<br>Stack Trace: <br>{ex.StackTrace}<br>");
            }
        }

        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            try
            {
                var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
                PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);                                    

                if (scenarioContext.TestError == null)
                {
                    if (stepType == "Given")
                        ExtentReportHelper.GetInstance(strAUT, driver).SetTestNodePassed(stepType, ScenarioStepContext.Current.StepInfo.Text);
                    else if (stepType == "When")
                        ExtentReportHelper.GetInstance(strAUT, driver).SetTestNodePassed(stepType, ScenarioStepContext.Current.StepInfo.Text);
                    else if (stepType == "Then")
                        ExtentReportHelper.GetInstance(strAUT, driver).SetTestNodePassed(stepType, ScenarioStepContext.Current.StepInfo.Text);
                    else if (stepType == "And")
                        ExtentReportHelper.GetInstance(strAUT, driver).SetTestNodePassed(stepType, ScenarioStepContext.Current.StepInfo.Text);
                }
                if (scenarioContext.TestError != null)
                {
                    var stacktrace = scenarioContext.TestError.StackTrace;
                    var errorMessage = "<pre>" + scenarioContext.TestError.Message + "</pre>";
                    var failureMessage = $"<br>{errorMessage}<br>Stack Trace: <br>{stacktrace}<br>";

                    ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, scenarioContext.TestError, driver);

                    if (stepType == "Given")
                        ExtentReportHelper.GetInstance(strAUT, driver).SetTestNodeFailed(stepType, ScenarioStepContext.Current.StepInfo.Text, failureMessage, CommonUtilities.commonUtilities.TakeScreenshot(driver, ScenarioStepContext.Current.StepInfo.Text));
                    if (stepType == "When")
                        ExtentReportHelper.GetInstance(strAUT, driver).SetTestNodeFailed(stepType, ScenarioStepContext.Current.StepInfo.Text, failureMessage, CommonUtilities.commonUtilities.TakeScreenshot(driver, ScenarioStepContext.Current.StepInfo.Text));
                    if (stepType == "Then")
                        ExtentReportHelper.GetInstance(strAUT, driver).SetTestNodeFailed(stepType, ScenarioStepContext.Current.StepInfo.Text, failureMessage, CommonUtilities.commonUtilities.TakeScreenshot(driver, ScenarioStepContext.Current.StepInfo.Text));
                    if (stepType == "And")
                        ExtentReportHelper.GetInstance(strAUT, driver).SetTestNodeFailed(stepType, ScenarioStepContext.Current.StepInfo.Text, failureMessage, CommonUtilities.commonUtilities.TakeScreenshot(driver, ScenarioStepContext.Current.StepInfo.Text));
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                ExtentReportHelper.GetInstance(strAUT, driver).SetTestStatusFail($"<br>{ex.Message}<br>Stack Trace: <br>{ex.StackTrace}<br>");
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
            try
            {                
                ExtentReportHelper.GetInstance(strAUT, driver).CloseExtentReport();
                CloseBrowser();                
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                ExtentReportHelper.GetInstance(strAUT, driver).SetTestStatusFail($"<br>{ex.Message}<br>Stack Trace: <br>{ex.StackTrace}<br>");
            }            
        }
    }
}
