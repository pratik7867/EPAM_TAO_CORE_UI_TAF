using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using EPAM_TAO_CORE_API_TAF.APIHelpers;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;
using System.Reflection;

namespace EPAM_TAO_API_TESTS.BaseAPITestConfig
{
    [TestClass]
    public abstract class BaseAPITestConfigurations
    {
        private string strAUT { get { return ConfigurationManager.AppSettings["AUT"].ToString(); } }
        private string strBaseURL { get { return ConfigurationManager.AppSettings["BaseURL"].ToString(); } }
        public TestContext TestContext { get; set; }
        public Exception testEx { get; set; }        

        public RestClient restClient;        

        [TestInitialize]
        public void Setup()
        {
            try
            {
                restClient = RestClientSetup.restClientSetup.SetupClient(strBaseURL);
                ExtentReportHelper.GetInstance(strAUT).CreateTest(TestContext.TestName);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                ExtentReportHelper.GetInstance(strAUT).SetTestStatusFail($"<br>{ex.Message}<br>Stack Trace: <br>{ex.StackTrace}<br>");
            }
        }

        [TestCleanup]
        public void Close()
        {
            try
            {
                var status = TestContext.CurrentTestOutcome;                

                switch (status)
                {
                    case UnitTestOutcome.Failed:
                        var errorMessage = "<pre>" + testEx.Message + "</pre>";
                        var stacktrace = testEx.StackTrace;                        
                        ExtentReportHelper.GetInstance(strAUT).SetTestStatusFail($"<br>{errorMessage}<br>Stack Trace: <br>{stacktrace}<br>");
                        ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, testEx);
                        break;
                    case UnitTestOutcome.Inconclusive:
                        ExtentReportHelper.GetInstance(strAUT).SetTestStatusSkipped();
                        break;
                    default:
                        ExtentReportHelper.GetInstance(strAUT).SetTestStatusPass();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                ExtentReportHelper.GetInstance(strAUT).SetTestStatusFail($"<br>{ex.Message}<br>Stack Trace: <br>{ex.StackTrace}<br>");
            }
            finally
            {
                ExtentReportHelper.GetInstance(strAUT).CloseExtentReport();
            }
        }
    }
}
