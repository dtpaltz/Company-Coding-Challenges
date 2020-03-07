using System.Web;

namespace DocumentRequestAPI.Helpers
{
	public class WebHelper
	{
		public static string GetBaseUrl()
		{
			var url = HttpContext.Current.Request.Url;
			return url.AbsoluteUri.Replace(url.AbsolutePath, string.Empty);
		}
	}
}