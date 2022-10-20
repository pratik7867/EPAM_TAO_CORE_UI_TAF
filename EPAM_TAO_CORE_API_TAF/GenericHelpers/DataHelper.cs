using System;
using System.Data;
using System.IO;
using System.Reflection;
using ExcelDataReader;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EPAM_TAO_CORE_API_TAF.APIHelpers
{
    public class DataHelper
    {
        private static readonly object syncLock = new object();
        private static DataHelper _dataHelper = null;

        DataHelper()
        {

        }

        public static DataHelper dataHelper
        {
            get
            {
                lock (syncLock)
                {
                    if (_dataHelper == null)
                    {
                        _dataHelper = new DataHelper();
                    }
                    return _dataHelper;
                }
            }
        }

        public DataTable GetAPIData(string strFileName, string strSheetName)
        {
            try
            {
                lock(syncLock)
                {
                    FileStream fileStream = File.Open(strFileName, FileMode.Open, FileAccess.Read);

                    IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);

                    DataSet dataSetResult = excelDataReader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    DataTableCollection dataTableCollection = dataSetResult.Tables;

                    DataTable dataTableResult = dataTableCollection[strSheetName];

                    fileStream.Close();

                    return dataTableResult;
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }             
        }

        public string GetAPIData(object instance, Dictionary<string, dynamic> dictOfReqData)
        {
            try
            {
                Type type = instance.GetType();

                foreach (var item in dictOfReqData)
                {
                    PropertyInfo propertyInfo = type.GetProperty(item.Key);
                    propertyInfo.SetValue(instance, item.Value, null);
                }

                return JsonConvert.SerializeObject(instance);
            }
            catch (Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
    }
}
