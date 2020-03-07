using DocumentRequestAPI.DataStore;
using DocumentRequestAPI.Models;
using System.Linq;
using System.Web.Http;

namespace DocumentRequestAPI.Controllers
{
	public class CallbackController : ApiController
    {
        // POST: api/Callback
        public void Post(PostModel value)
        {
			if (value.Body.Equals("STARTED"))
			{
				// request received by 3rd party service
			}
        }

		// PUT: api/Callback/5
		public void Put(int id, PutModel value)
        {
			var requests = DataStoreManager.DeserializeObjectsFromStore<RequestModel>();
			var targetRequest = requests.Where(r => r.id == id).FirstOrDefault();

			if (targetRequest != null)
			{
				targetRequest.status = value.Status;
				targetRequest.detail = value.Detail;

				DataStoreManager.SerializeJsonToStore<RequestModel>(requests);
			}
		}
    }
}
