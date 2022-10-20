using TechTalk.SpecFlow;
using FluentAssertions;
using RestSharp;
using EPAM_TAO_CORE_API_TAF.APIHelpers;
using EPAM_TAO_BDD_API_TESTS.APITests.Steps.Common;
using EPAM_TAO_BDD_API_TESTS.APIClasses.APIRequestClasses.Posts;
using EPAM_TAO_BDD_API_TESTS.APIClasses.APIResponseClasses.Posts;


namespace EPAM_TAO_BDD_API_TESTS.APITests.Steps.Posts
{
    [Binding]
    public sealed class PostsStepDefinition : CommonStepDefinition
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        public PostsStepDefinition(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        #region Create Post API

        [Given(@"I have the baseURL '(.*)'")]
        public void GivenIHaveTheBaseURL(string strBaseURL)
        {
            restClient = RestClientSetup.restClientSetup.SetupClient(strBaseURL);
        }

        [Given(@"I create a post using resource '(.*)', id '(.*)', title '(.*)' and author '(.*)'")]
        public void GivenICreateAPostUsingResourceIdTitleAndAuthor(string strResource, int intId, string strTitle, string strAuthor)
        {
            dictOfPostsReqData.Add("id", intId);
            dictOfPostsReqData.Add("title", strTitle);
            dictOfPostsReqData.Add("author", strAuthor);
            requestData = DataHelper.dataHelper.GetAPIData(new CreatePostsReq(), dictOfPostsReqData);

            restRequest = RequestHelper.requestHelper.CreatePostRequest(strResource, requestData, DataFormat.Json);
        }

        [When(@"I execute the create post request")]
        public void WhenIExecuteTheCreatePostRequest()
        {
            restResponse = ResponseHelper.responseHelper.ExecuteRequest(restClient, restRequest);
        }

        [Then(@"I should be able to get the post with id '(.*)', title '(.*)', author '(.*)' and status '(.*)'")]
        public void ThenIShouldBeAbleToGetThePostWithIdTitleAuthorAndStatus(int expectedId, string expectedTitle, string expectedAuthor, int expectedStatus)
        {
            var actualResponseContent = ResponseHelper.responseHelper.GetContent<CreatePostsResp>(restResponse);
            var actualResponseStatusCode = ResponseHelper.responseHelper.GetStatusCode(restResponse);            

            var actualPostId = actualResponseContent.id;
            var actualPostTitle = actualResponseContent.title;
            var actualPostAuthor = actualResponseContent.author;

            actualResponseStatusCode.Should().Be(expectedStatus);
            actualPostId.Should().Be(expectedId);
            actualPostTitle.Should().BeEquivalentTo(expectedTitle);
            actualPostAuthor.Should().BeEquivalentTo(expectedAuthor);
        }

        #endregion

        #region Update Post API

        [Given(@"I update a post using resource '(.*)', id '(.*)', title '(.*)' and author '(.*)'")]
        public void GivenIUpdateAPostUsingResourceIdTitleAndAuthor(string strResource, int intId, string strTitle, string strAuthor)
        {
            dictOfPostsReqData.Add("id", intId);
            dictOfPostsReqData.Add("title", strTitle);
            dictOfPostsReqData.Add("author", strAuthor);
            requestData = DataHelper.dataHelper.GetAPIData(new UpdatePostsReq(), dictOfPostsReqData);

            restRequest = RequestHelper.requestHelper.CreatePutRequest(strResource, requestData, DataFormat.Json);
        }

        [When(@"I execute the update post request")]
        public void WhenIExecuteTheUpdatePostRequest()
        {
            restResponse = ResponseHelper.responseHelper.ExecuteRequest(restClient, restRequest);
        }

        [Then(@"I should be able to get the updated post with id '(.*)', title '(.*)', author '(.*)' and status '(.*)'")]
        public void ThenIShouldBeAbleToGetTheUpdatedPostWithIdTitleAuthorAndStatus(int expectedId, string expectedTitle, string expectedAuthor, int expectedStatus)
        {
            var actualResponseContent = ResponseHelper.responseHelper.GetContent<UpdatePostsResp>(restResponse);
            var actualResponseStatusCode = ResponseHelper.responseHelper.GetStatusCode(restResponse);

            var actualPostId = actualResponseContent.id;
            var actualPostTitle = actualResponseContent.title;
            var actualPostAuthor = actualResponseContent.author;

            actualResponseStatusCode.Should().Be(expectedStatus);
            actualPostId.Should().Be(expectedId);
            actualPostTitle.Should().BeEquivalentTo(expectedTitle);
            actualPostAuthor.Should().BeEquivalentTo(expectedAuthor);
        }

        #endregion

        #region Delete Post API

        [Given(@"I delete a post using resource '(.*)'")]
        public void GivenIDeleteAPostUsingResource(string strResource)
        {
            restRequest = RequestHelper.requestHelper.CreateDeleteRequest(strResource);
        }

        [When(@"I execute the delete post request")]
        public void WhenIExecuteTheDeletePostRequest()
        {
            restResponse = ResponseHelper.responseHelper.ExecuteRequest(restClient, restRequest);
        }

        [Then(@"I should get the status '(.*)'")]
        public void ThenIShouldGetTheStatus(int expectedStatus)
        {
            var actualResponseStatusCode = ResponseHelper.responseHelper.GetStatusCode(restResponse);

            actualResponseStatusCode.Should().Be(expectedStatus);
        }

        #endregion
    }
}
