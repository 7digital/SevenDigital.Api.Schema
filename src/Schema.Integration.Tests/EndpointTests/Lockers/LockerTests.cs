using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Schema.Lockers;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Lockers
{
	[TestFixture]
	[Category("Integration")]
	public class LockerTests
	{
		private IApi _api;
		private LockerRelease _availableRelease;

		[TestFixtureSetUp]
		public void Setup()
		{
			SetupAsync().Wait();
		}

		private async Task SetupAsync()
		{
			_api = new ApiConnection();
			var request =
			_api.Create<Locker>()
				.ForUser(TestDataFromEnvironmentOrAppSettings.AccessToken, TestDataFromEnvironmentOrAppSettings.AccessTokenSecret)
				.WithParameter("country", "GB")
				.WithParameter("usageTypes", "download");
			var locker = await request.Please();
			Assert.That(locker, Is.Not.Null);
			_availableRelease = locker.Response.LockerReleases.First(o => o.Available);
		}

		[Test]
		public void Returns_download_element()
		{
			Assert.That(_availableRelease.Release.Download, Is.Not.Null);
		}

		[Test]
		public void Returns_release_date()
		{
			Assert.That(_availableRelease.Release.Download.ReleaseDate, Is.Not.EqualTo(default(DateTime)));
		}

		[Test]
		public void Returns_format_in_downloadUrl()
		{
			Assert.That(_availableRelease.LockerTracks[0].DownloadUrls[0].Format.Description, Is.Not.Null);
		}
	}
}
