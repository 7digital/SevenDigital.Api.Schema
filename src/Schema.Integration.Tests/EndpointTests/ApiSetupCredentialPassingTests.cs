using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests
{
	[TestFixture]
	public class ApiSetupCredentialPassingTests
	{
		[Test]
		public async Task Can_hit_endpoint_if_I_pass_credentials_into_api()
		{
			IApi api = new ApiConnection();
			var request = api.Create<Status>();
			var status = await request.Please();

			Assert.That(status, Is.Not.Null);
			Assert.That(status.ServerTime.Day, Is.EqualTo(DateTime.Now.Day));
		}

		[Test]
		public async Task Can_hit_endpoint_if_I_pass_credentials_into_static_api()
		{
			StaticApiFactory.Factory = new ApiConnection();

			var request = Api<Status>.Create;
			var status = await request.Please();

			Assert.That(status, Is.Not.Null);
			Assert.That(status.ServerTime.Day, Is.EqualTo(DateTime.Now.Day));
		}
	}
}