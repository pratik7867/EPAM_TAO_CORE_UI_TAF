using System;
using System.Reflection;
using RestSharp;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;

namespace EPAM_TAO_CORE_API_TAF.APIHelpers
{
    public class RestClientSetup
    {
        private static readonly object syncLock = new object();
        private static RestClientSetup _restClientSetup = null;

        private RestClient restClient;

        RestClientSetup()
        {

        }

        public static RestClientSetup restClientSetup
        {
            get
            {
                lock (syncLock)
                {
                    if (_restClientSetup == null)
                    {
                        _restClientSetup = new RestClientSetup();
                    }
                    return _restClientSetup;
                }
            }
        }

        public RestClient SetupClient(string strBaseURL)
        {
            try
            {
                if (restClient == null)
                {
                    restClient = new RestClient(strBaseURL);
                }

                return restClient;
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }            
        }
    }
}
