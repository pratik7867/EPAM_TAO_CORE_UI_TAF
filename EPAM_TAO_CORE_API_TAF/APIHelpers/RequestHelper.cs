using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;

namespace EPAM_TAO_CORE_API_TAF.APIHelpers
{
    public class RequestHelper
    {
        private static readonly object syncLock = new object();
        private static RequestHelper _requestHelper = null;

        private RestRequest restRequest;
        private Dictionary<string, string> DictOfRequestHeaders;

        RequestHelper()
        {

        }

        public static RequestHelper requestHelper
        {
            get
            {
                lock (syncLock)
                {
                    if (_requestHelper == null)
                    {
                        _requestHelper = new RequestHelper();
                    }
                    return _requestHelper;
                }
            }
        }


        private Dictionary<string, string> getRequestHeaders()
        {
            try
            {
                if (DictOfRequestHeaders == null)
                {
                    DictOfRequestHeaders = new Dictionary<string, string>();

                    DictOfRequestHeaders.Add("Accept", "*/*");
                    DictOfRequestHeaders.Add("Content-Type", "application/json");
                }

                return DictOfRequestHeaders;
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            
        }

        #region GET REQUEST
        public RestRequest CreateGetRequest(string strResource)
        {
            try
            {
                restRequest = new RestRequest(strResource, Method.Get);

                foreach (var item in getRequestHeaders())
                {
                    if (item.Key != "Content-Type")
                    {
                        restRequest.AddHeader(item.Key, item.Value);
                    }
                }

                return restRequest;
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
        }

        #endregion

        #region POST REQUEST
        public RestRequest CreatePostRequest(string strResource, string strPayload, DataFormat dataFormat)
        {
            try
            {
                restRequest = new RestRequest(strResource, Method.Post);
                restRequest.RequestFormat = dataFormat;

                foreach (var item in getRequestHeaders())
                {
                    restRequest.AddHeader(item.Key, item.Value);
                }

                restRequest.AddBody(strPayload);

                return restRequest;
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        #endregion

        #region PUT REQUEST
        public RestRequest CreatePutRequest(string strResource, string strPayload, DataFormat dataFormat)
        {
            try
            {
                restRequest = new RestRequest(strResource, Method.Put);
                restRequest.RequestFormat = dataFormat;

                foreach (var item in getRequestHeaders())
                {
                    restRequest.AddHeader(item.Key, item.Value);
                }

                restRequest.AddBody(strPayload);

                return restRequest;
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        #endregion

        #region PATCH REQUEST
        public RestRequest CreatePatchRequest(string strResource, string strPayload, DataFormat dataFormat)
        {
            try
            {
                restRequest = new RestRequest(strResource, Method.Patch);
                restRequest.RequestFormat = dataFormat;

                foreach (var item in getRequestHeaders())
                {
                    restRequest.AddHeader(item.Key, item.Value);
                }

                restRequest.AddBody(strPayload);

                return restRequest;
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        #endregion

        #region DELETE REQUEST
        public RestRequest CreateDeleteRequest(string strResource)
        {
            try
            {
                restRequest = new RestRequest(strResource, Method.Delete);

                foreach (var item in getRequestHeaders())
                {
                    if (item.Key != "Content-Type")
                    {
                        restRequest.AddHeader(item.Key, item.Value);
                    }
                }

                return restRequest;
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;                
            }
        }

        #endregion

    }
}
