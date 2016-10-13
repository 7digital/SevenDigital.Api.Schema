using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Lockers
{
	[TestFixture]
	public class LockerTests
	{
		private IApi _api;

		[TestFixtureSetUp]
		public void Setup()
		{
			_api = new ApiConnection();
		}

		[Test]
		public async Task Can_get_release_date()
		{
			var request =
				_api.Create<Schema.Lockers.Locker>()
				.ForUser(
					TestDataFromEnvironmentOrAppSettings.AccessToken,
					TestDataFromEnvironmentOrAppSettings.AccessTokenSecret)
					.WithParameter("country", "GB");
	
			var locker = await request.Please();

			Assert.That(locker, Is.Not.Null);
			Assert.That(locker.Response.LockerReleases.First().Release.ReleaseDate, Is.Not.Null);
		}
	}
}
