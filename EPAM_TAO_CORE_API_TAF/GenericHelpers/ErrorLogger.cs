using System;
using System.IO;
using System.Reflection;

namespace EPAM_TAO_CORE_UI_TAF.UI_Helpers
{
    public class ErrorLogger
    {
        private static readonly object syncLock = new object();
        private static ErrorLogger _errorLogger = null;

        private string strPathToErrorLogFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DateTime.Now.ToString("dd-MM-yyyy")) + @"\" + "ErrorLogs";
        private string strPathToErrorLogFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DateTime.Now.ToString("dd-MM-yyyy")) + @"\" + "ErrorLogs" + @"\" + "ErrorLog_" + DateTime.Now.ToString("dd-MM-yyyy");        

        ErrorLogger()
        {

        }

        public static ErrorLogger errorLogger
        {
            get
            {
                lock (syncLock)
                {
                    if (_errorLogger == null)
                    {
                        _errorLogger = new ErrorLogger();
                    }
                    return _errorLogger;
                }
            }
        }

        public void ErrorLog(string strMethodName, Exception errorEx)
        {
            try
            {
                lock(syncLock)
                {
                    if(!Directory.Exists(strPathToErrorLogFolder))
                    {
                        Directory.CreateDirectory(strPathToErrorLogFolder);
                    }

                    using (StreamWriter streamWriter = File.AppendText(strPathToErrorLogFile))
                    {
                        streamWriter.AutoFlush = true;

                        streamWriter.WriteLine("Date, Time: {0}, {1}", DateTime.Now.ToString("dd-MM-yyyy"), DateTime.Now.ToShortTimeString());
                        streamWriter.WriteLine("Method: " + strMethodName);                        
                        streamWriter.WriteLine("Exception Message: " + errorEx.Message);
                        streamWriter.WriteLine("\n");
                        streamWriter.WriteLine("Exception Stack Trace: " + errorEx.StackTrace);
                        streamWriter.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        streamWriter.WriteLine("\n");
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
        
    }
}
