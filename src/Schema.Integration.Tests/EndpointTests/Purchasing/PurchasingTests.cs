using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Schema.Users;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Purchasing
{
	[TestFixture]
	[Category("Integration")]
	public class PurchasingTests
	{
		private IApi _api;
		private UserDeliverItem _response;

		[TestFixtureSetUp]
		public void Setup()
		{
			SetupAsync().Wait();
		}

		private async Task SetupAsync()
		{
			_api = new ApiConnection(new PurchasingAppSettingsCredentials());

			var request =
				_api.Create<UserDeliverItem>()
					.ForUser(
						TestDataFromEnvironmentOrAppSettings.PurchasingAccessToken,
						TestDataFromEnvironmentOrAppSettings.PurchasingAccessTokenSecret)
					.WithParameter("country", "GB")
					.WithParameter("transactionId", string.Format("PurchEndToEnd:{0:o}", DateTime.UtcNow))
					.WithParameter("releaseId", TestDataFromEnvironmentOrAppSettings.PurchasingReleaseId);
			var userDeliverItemResponse = await request.Please();
			Assert.That(userDeliverItemResponse, Is.Not.Null);
			_response = userDeliverItemResponse;
		}

		[Test]
		public void Returns_purchase_id()
		{
			Assert.That(_response.Id, Is.Not.Null);
		}
	}
}
