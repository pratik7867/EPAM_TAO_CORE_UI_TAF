using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using EPAM_TAO_CORE_API_TAF.APIHelpers;
using EPAM_TAO_API_TESTS.BaseAPITestConfig;
using EPAM_TAO_API_TESTS.APIClasses.APIRequestClasses.Posts;
using EPAM_TAO_API_TESTS.APIClasses.APIResponseClasses.Posts;

namespace EPAM_TAO_API_TESTS.APITests
{
    /*
     *Order of Test Execution -
     * CreatePostAPI()
     * UpdatePostAPI()
     * DeletePostAPI()
     */

    [TestClass]
    public class PostsAPITestClass : BaseAPITestConfigurations
    {        
        Dictionary<string, dynamic> dictOfPostsReqData = new Dictionary<string, dynamic>();        

        [TestMethod]
        public void A_CreatePostAPI()
        {
            try
            {
                #region ARRANGE

                string resource = "/posts";

                dictOfPostsReqData.Add("id", 2);
                dictOfPostsReqData.Add("title", "json-server2");
                dictOfPostsReqData.Add("author", "typicode2");
                var requestData = DataHelper.dataHelper.GetAPIData(new CreatePostsReq(), dictOfPostsReqData);

                int expectedResponseCode = 201;

                #endregion

                #region ACT

                var restRequest = RequestHelper.requestHelper.CreatePostRequest(resource, requestData, DataFormat.Json);

                var restResponse = ResponseHelper.responseHelper.ExecuteRequest(restClient, restRequest);

                var actualResponseContent = ResponseHelper.responseHelper.GetContent<CreatePostsResp>(restResponse);
                var actualResponseStatusCode = ResponseHelper.responseHelper.GetStatusCode(restResponse);

                #endregion

                #region ASSERT

                Assert.AreEqual(expectedResponseCode, actualResponseStatusCode);
                Assert.AreEqual(dictOfPostsReqData["id"], actualResponseContent.id, nameof(CreatePostsResp.id) + "did not match");
                Assert.AreEqual(dictOfPostsReqData["title"], actualResponseContent.title, nameof(CreatePostsResp.title) + "did not match");
                Assert.AreEqual(dictOfPostsReqData["author"], actualResponseContent.author, nameof(CreatePostsResp.author) + "did not match");
            }
            catch(Exception ex)
            {                
                testEx = ex;
                Assert.Fail(ex.Message);
            }

            #endregion
        }

        [TestMethod]
        public void B_UpdatePostAPI()
        {
            try
            {
                #region ARRANGE

                string resource = "/posts/2";

                dictOfPostsReqData.Add("id", 2);
                dictOfPostsReqData.Add("title", "json-server3");
                dictOfPostsReqData.Add("author", "typicode3");
                var requestData = DataHelper.dataHelper.GetAPIData(new UpdatePostsReq(), dictOfPostsReqData);

                int expectedResponseCode = 200;

                #endregion

                #region ACT

                var restRequest = RequestHelper.requestHelper.CreatePutRequest(resource, requestData, DataFormat.Json);

                var restResponse = ResponseHelper.responseHelper.ExecuteRequest(restClient, restRequest);

                var actualResponseContent = ResponseHelper.responseHelper.GetContent<UpdatePostsResp>(restResponse);
                var actualResponseStatusCode = ResponseHelper.responseHelper.GetStatusCode(restResponse);

                #endregion

                #region ASSERT

                Assert.AreEqual(expectedResponseCode, actualResponseStatusCode);
                Assert.AreEqual(dictOfPostsReqData["id"], actualResponseContent.id, nameof(UpdatePostsResp.id) + "did not match");
                Assert.AreEqual(dictOfPostsReqData["title"], actualResponseContent.title, nameof(UpdatePostsResp.title) + "did not match");
                Assert.AreEqual(dictOfPostsReqData["author"], actualResponseContent.author, nameof(UpdatePostsResp.author) + "did not match");

                #endregion
            }
            catch (Exception ex)
            {
                testEx = ex;
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void C_DeletePostAPI()
        {
            try
            {
                #region ARRANGE

                string resource = "/posts/2";
                int expectedResponseCode = 200;

                #endregion

                #region ACT

                var restRequest = RequestHelper.requestHelper.CreateDeleteRequest(resource);

                var restResponse = ResponseHelper.responseHelper.ExecuteRequest(restClient, restRequest);

                var actualResponseStatusCode = ResponseHelper.responseHelper.GetStatusCode(restResponse);

                #endregion

                #region ASSERT

                Assert.AreEqual(expectedResponseCode, actualResponseStatusCode);

                #endregion
            }
            catch (Exception ex)
            {
                testEx = ex;
                Assert.Fail(ex.Message);
            }
        }
    }
}
