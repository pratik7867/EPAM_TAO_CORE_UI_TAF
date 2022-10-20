using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using RestSharp;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;

namespace EPAM_TAO_CORE_API_TAF.APIHelpers
{
    public class ResponseHelper
    {
        private static readonly object syncLock = new object();
        private static ResponseHelper _responseHelper = null;

        ResponseHelper()
        {

        }

        public static ResponseHelper responseHelper
        {
            get
            {
                lock (syncLock)
                {
                    if (_responseHelper == null)
                    {
                        _responseHelper = new ResponseHelper();
                    }
                    return _responseHelper;
                }
            }
        }

        public RestResponse ExecuteRequest(RestClient restClient, RestRequest restRequest)
        {
            try
            {
                return restClient.Execute(restRequest);
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public DC GetContent<DC>(RestResponse restResponse)
        {
            try
            {
               return JsonConvert.DeserializeObject<DC>(restResponse.Content);
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public int GetStatusCode(RestResponse restResponse)
        {
            try
            {
                HttpStatusCode httpStatusCode = restResponse.StatusCode;
                return (int)httpStatusCode;
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public string GetStatusDescription(RestResponse restResponse)
        {
            try
            {
                return restResponse.StatusDescription;
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public bool GetRequestSuccessFlag(RestResponse restResponse)
        {
            try
            {
                return restResponse.IsSuccessful;
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public IList<Parameter> GetResponseHeaders(RestResponse restResponse)
        {
            try
            {
                return (IList<Parameter>)restResponse.Headers;
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public IList<Cookie> GetResponseCookies(RestResponse restResponse)
        {
            try
            {
                return (IList<Cookie>)restResponse.Cookies;
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
    }
}
