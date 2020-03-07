using DocumentRequestAPI.DataStore;
using DocumentRequestAPI.Helpers;
using DocumentRequestAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace DocumentRequestAPI.Controllers
{
	public class RequestController : ApiController
    {
        // POST: api/Request
        public void Post(RequestModel value)
        {
			value.callback = WebHelper.GetBaseUrl() + @"/api/Callback";

			// serialize the [body, callback] fields for the new request
			var includedProperties = new List<string>() { nameof(RequestModel.body), nameof(RequestModel.callback) };
			var contractResolver = new DynamicContractResolver(includedProperties);
			string postPayload = JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings { ContractResolver = contractResolver });

			// send the post request to the 3rd party service and store the result
			bool requestSent = SendRequest(postPayload);

			// if the request was successfull, then store the state of the new request
			//if (requestSent)
			{
				var requests = DataStoreManager.DeserializeObjectsFromStore<RequestModel>();
				value.id = requests.Count + 1;
				requests.Add(value);
				DataStoreManager.SerializeJsonToStore<RequestModel>(requests);
			}
		}

		private bool SendRequest(string requestPayload)
		{
			bool result = false;
			string serviceStubbUrl = @"https://urldefense.proofpoint.com/v2/url?u=http-3A__example.com_request&d=DwIGAg&c=iWzD8gpC8N4xSkOHZBDmCw&r=R0U6eziUSfkIiSy6xlVVHEbyT-5CVX85B2177L6G3Po&m=yeOGbdLEit9cyYWgLXxv5PRcMgRiallgPowRbt59hFw&s=lZ8qcf2Nw6VP2qI311Xp3wnZgZDhuaIrUg7krpQgTr4&e=";

			using (var httpClient = new HttpClient())
			{
				using (var request = new HttpRequestMessage(new HttpMethod("POST"), serviceStubbUrl))
				{
					request.Content = new StringContent(requestPayload, Encoding.Default, @"application/x-www-form-urlencoded");

					var response = httpClient.SendAsync(request).Result;
					result = response.IsSuccessStatusCode;
				}
			}

			return result;
		}
    }
}
