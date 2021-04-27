using System;
using Newtonsoft.Json.Linq;
using RestSharp;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace API_App
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var restClient = new RestClient("https://api.postcodes.io");
            var restRequest = new RestRequest();
            restRequest.Method = Method.GET;
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.Timeout = -1;

            var postcode = "EC2Y 5AS";
            restRequest.Resource = $"postcodes/{postcode.ToLower().Replace(" ", "")}";

            var restResponse = restClient.Execute(restRequest);
            Console.WriteLine("Response Content (string):");
            Console.WriteLine(restResponse.Content);
            Console.WriteLine("\n\n");

            var jsonRespone = JObject.Parse(restResponse.Content);
            Console.WriteLine("\nResponse content as a JObject");
            Console.WriteLine(jsonRespone);
            var adminDistrict = jsonRespone["result"]["admin_district"];
            var paliamentaryConstituency = jsonRespone["result"]["paliamentary constituency"];
            var adminWard = jsonRespone["result"]["admin_ward"];
            Console.WriteLine(adminDistrict); 
            Console.WriteLine(paliamentaryConstituency);
            Console.WriteLine(adminWard);
            var singlePostcodeResponse = JsonConvert.DeserializeObject<SinglePostcodeResponse>(restResponse.Content);
            
                
                //var client = new RestClient("https://api.postcodes.io/postcodes/EC2Y5AS");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Cookie", "__cfduid=d07ff89d6ceb11516cec847fedae7636c1619439989");
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            var client = new RestClient("https://api.postcodes.io/postcodes/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "__cfduid=d07ff89d6ceb11516cec847fedae7636c1619439989");
            request.AddParameter("application/json", "{\r\n    \"postcodes\" : [\"OX49 5NU\", \"M32 0JG\", \"NE30 1DP\"]\r\n}", ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);

            var bulkJsonResponse = JObject.Parse(response.Content);

            var bulkPostcodeJsonResponse = JsonConvert.DeserializeObject<BulkPostcodeResponse>(response.Content);

            var adminDistrictFromBPR = bulkPostcodeJsonResponse.result[1].result.admin_district;

            var quality = bulkPostcodeJsonResponse.result[0].result.quality;
        }
    }
}
