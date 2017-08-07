using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Schema.Purchasing;
using SevenDigital.Api.Schema.Users;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Purchasing
{
	[TestFixture]
	[Category("Integration")]
	public class PurchasingTests
	{
		private IApi _api;
		private LockerRelease _lockerRelease;

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
			_lockerRelease = userDeliverItemResponse.LockerReleases.First();
		}

		[Test]
		public void Returns_release_date()
		{
			Assert.That(_lockerRelease.Release.ReleaseDate, Is.Not.EqualTo(default(DateTime)));
		}

		[Test]
		public void Returns_urls()
		{
			Assert.That(_lockerRelease.Release.Url, Is.Not.Null.And.Not.Empty);
			Assert.That(_lockerRelease.Release.Artist.Url, Is.Not.Null.And.Not.Empty);
		}

		[Test]
		public void Has_presigned_download_url()
		{
			Assert.That(_lockerRelease.LockerTracks.First().DownloadUrls.First().Url, Is.StringContaining("oauth_signature"));
		}

		[Test]
		public void Returns_format_in_downloadUrl()
		{
			Assert.That(_lockerRelease.LockerTracks[0].DownloadUrls[0].Format.FileFormat, Is.Not.Null.And.Not.Empty);
			Assert.That(_lockerRelease.LockerTracks[0].DownloadUrls[0].Format.BitRate, Is.Not.Null.And.Not.Empty);
			Assert.That(_lockerRelease.LockerTracks[0].DownloadUrls[0].Format.DrmFree, Is.True);
		}
	}
}
