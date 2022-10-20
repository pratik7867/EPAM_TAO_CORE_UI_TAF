using System;
using System.Reflection;
using System.Configuration;
using TechTalk.SpecFlow;
using EPAM_TAO_CORE_API_TAF.APIHelpers;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;

namespace EPAM_TAO_BDD_API_TESTS.Hooks
{   
    [Binding]
    public sealed class TestHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks                
        private string strAUT { get { return ConfigurationManager.AppSettings["AUT"].ToString(); } }                

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            //TODO: implement logic that has to run before executing each scenario
            try
            {
                ExtentReportHelper.GetInstance(strAUT).CreateTest(scenarioContext.ScenarioInfo.Title);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                ExtentReportHelper.GetInstance(strAUT).SetTestStatusFail($"<br>{ex.Message}<br>Stack Trace: <br>{ex.StackTrace}<br>");
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
                        ExtentReportHelper.GetInstance(strAUT).SetTestNodePassed(stepType, ScenarioStepContext.Current.StepInfo.Text);
                    else if (stepType == "When")
                        ExtentReportHelper.GetInstance(strAUT).SetTestNodePassed(stepType, ScenarioStepContext.Current.StepInfo.Text);
                    else if (stepType == "Then")
                        ExtentReportHelper.GetInstance(strAUT).SetTestNodePassed(stepType, ScenarioStepContext.Current.StepInfo.Text);
                    else if (stepType == "And")
                        ExtentReportHelper.GetInstance(strAUT).SetTestNodePassed(stepType, ScenarioStepContext.Current.StepInfo.Text);
                }
                if (scenarioContext.TestError != null)
                {
                    var stacktrace = scenarioContext.TestError.StackTrace;
                    var errorMessage = "<pre>" + scenarioContext.TestError.Message + "</pre>";
                    var failureMessage = $"<br>{errorMessage}<br>Stack Trace: <br>{stacktrace}<br>";

                    ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, scenarioContext.TestError);

                    if (stepType == "Given")
                        ExtentReportHelper.GetInstance(strAUT).SetTestNodeFailed(stepType, ScenarioStepContext.Current.StepInfo.Text, failureMessage);
                    if (stepType == "When")
                        ExtentReportHelper.GetInstance(strAUT).SetTestNodeFailed(stepType, ScenarioStepContext.Current.StepInfo.Text, failureMessage);
                    if (stepType == "Then")
                        ExtentReportHelper.GetInstance(strAUT).SetTestNodeFailed(stepType, ScenarioStepContext.Current.StepInfo.Text, failureMessage);
                    if (stepType == "And")
                        ExtentReportHelper.GetInstance(strAUT).SetTestNodeFailed(stepType, ScenarioStepContext.Current.StepInfo.Text, failureMessage);
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                ExtentReportHelper.GetInstance(strAUT).SetTestStatusFail($"<br>{ex.Message}<br>Stack Trace: <br>{ex.StackTrace}<br>");
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
            try
            {                
                ExtentReportHelper.GetInstance(strAUT).CloseExtentReport();                             
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                ExtentReportHelper.GetInstance(strAUT).SetTestStatusFail($"<br>{ex.Message}<br>Stack Trace: <br>{ex.StackTrace}<br>");
            }            
        }
    }
}
