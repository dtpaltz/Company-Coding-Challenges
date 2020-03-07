using DocumentRequestAPI.DataStore;
using DocumentRequestAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DocumentRequestAPI.Controllers
{
	public class StatusController : ApiController
    {
        // GET: api/Status/5
        public string Get(int id)
        {
			var requests = DataStoreManager.DeserializeObjectsFromStore<RequestModel>();
			var targetRequest = requests.Where(r => r.id == id).FirstOrDefault();

			string result = string.Empty;
			if (targetRequest != null)
			{
				// serialize the [status, detail, body] fields for the target request
				var includedProperties = new List<string>() { nameof(RequestModel.status), nameof(RequestModel.detail), nameof(RequestModel.body) };
				var contractResolver = new DynamicContractResolver(includedProperties);
				result = JsonConvert.SerializeObject(targetRequest, Formatting.None, new JsonSerializerSettings { ContractResolver = contractResolver });
			}

			return result;
		}
	}
}
