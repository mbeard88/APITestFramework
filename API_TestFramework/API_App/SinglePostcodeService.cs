using System;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API_App
{
    public class SinglePostcodeService
    {
        #region properties
        public RestClient Client;
        public IRestResponse RestResponse { get; set; }
        public JObject ResponseContent { get; set; }
        public SinglePostcodeResponse ResponseObject { get; set; }
        public string PostcodeSelected { get; set; }

        #endregion

        // constructor - create the RestClient object
        public SinglePostcodeService()
        {
            Client = new RestClient { BaseUrl = new Uri(AppConfigReader.BaseUrl) };
        }
        public async Task MakeRequestAsync(string postcode)
        {
            // Set request
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            PostcodeSelected = postcode;

            //define request resource path
            request.Resource = $"postcodes/{postcode.ToLower().Replace(" ", "")}";

            RestResponse = await Client.ExecuteAsync(request);

            ResponseContent = JObject.Parse(RestResponse.Content);

            // parse response body into an object tree
            ResponseObject = JsonConvert.DeserializeObject<SinglePostcodeResponse>(RestResponse.Content);

        }
    }
}
