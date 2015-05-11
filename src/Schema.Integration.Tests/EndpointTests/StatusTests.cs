using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests
{
	[TestFixture]
	public class StatusTests
	{
		private readonly IApi _api = new ApiConnection();
		
		[Test]
		public async Task Can_hit_endpoint()
		{
			var status = await _api.Create<Status>().Please();

			Assert.That(status, Is.Not.Null);
			Assert.That(status.ServerTime.Day, Is.EqualTo(DateTime.Now.Day));
		}
	}
}
