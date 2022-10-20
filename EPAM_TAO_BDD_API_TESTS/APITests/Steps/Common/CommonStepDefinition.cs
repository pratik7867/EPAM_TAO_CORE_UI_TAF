using System.Collections.Generic;
using TechTalk.SpecFlow;
using RestSharp;

namespace EPAM_TAO_BDD_API_TESTS.APITests.Steps.Common
{
    [Binding]
    public class CommonStepDefinition
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        protected readonly ScenarioContext scenarioContext;

        protected RestClient restClient;
        protected RestRequest restRequest;
        protected RestResponse restResponse;
        protected Dictionary<string, dynamic> dictOfPostsReqData;
        protected string requestData;

        public CommonStepDefinition(ScenarioContext _scenarioContext)
        {
            scenarioContext = _scenarioContext;
            dictOfPostsReqData = new Dictionary<string, dynamic>();
        }
    }
}
