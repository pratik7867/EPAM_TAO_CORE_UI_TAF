using System;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;

namespace EPAM_TAO_CORE_API_TAF.APIHelpers
{
    public class ExtentReportHelper
    {
        private static readonly object syncLock = new object();
        private static ExtentReportHelper _extentReportHelper = null;

        private ExtentReports extent { get; set; }
        private ExtentHtmlReporter reporter { get; set; }
        private ExtentTest test { get; set; }

        ExtentReportHelper(string strAUT)
        {
            try
            {
                extent = new ExtentReports();

                reporter = new ExtentHtmlReporter(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DateTime.Now.ToString("dd-MM-yyyy")) + @"\" + "TAF_Report.html");
                reporter.Config.DocumentTitle = "API Automation Testing Report";
                reporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
                extent.AttachReporter(reporter);

                extent.AddSystemInfo("Application Under Test", strAUT);
                extent.AddSystemInfo("Environment", "QA");
                extent.AddSystemInfo("Machine", Environment.MachineName);
                extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }        

        public static ExtentReportHelper GetInstance(string strAUT)
        {
            lock (syncLock)
            {
                if (_extentReportHelper == null)
                {
                    _extentReportHelper = new ExtentReportHelper(strAUT);
                }
                return _extentReportHelper;
            }
        }

        public void CreateTest(string testName)
        {
            try
            {
                test = extent.CreateTest(testName);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
        public void SetStepStatusPass(string stepDescription)
        {
            try
            {
                test.Log(Status.Pass, stepDescription);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
        public void SetStepStatusWarning(string stepDescription)
        {
            try
            {
                test.Log(Status.Warning, stepDescription);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
        public void SetTestStatusPass()
        {
            try
            {
                test.Pass("Test Executed Sucessfully!");
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
        public void SetTestStatusFail(string message)
        {
            try
            {
                var printMessage = "<p><b>Test FAILED!</b></p>" + $"Message: <br>{message}<br>";
                test.Fail(printMessage);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }        
        public void SetTestStatusSkipped()
        {
            try
            {
                test.Skip("Test skipped!");
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
        public void SetTestNodePassed(string gherkinKeyword, string stepInfo)
        {
            try
            {
                test.CreateNode(gherkinKeyword, stepInfo).Pass("Step Executed Sucessfully!");
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
        public void SetTestNodeFailed(string gherkinKeyword, string stepInfo, string message)
        {
            try
            {
                var printMessage = "<p><b>Test FAILED!</b></p>" + $"Message: <br>{message}<br>";
                test.CreateNode(gherkinKeyword, stepInfo).Fail("Step Failed!");                
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public void CloseExtentReport()
        {
            try
            {
                extent.Flush();
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
    }
}
