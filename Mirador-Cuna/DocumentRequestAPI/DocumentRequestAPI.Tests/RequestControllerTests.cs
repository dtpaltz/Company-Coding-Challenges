using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocumentRequestAPI.Tests
{
	[TestClass]
	public class RequestControllerTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			var url = Helpers.WebHelper.GetBaseUrl();
		}
	}
}
