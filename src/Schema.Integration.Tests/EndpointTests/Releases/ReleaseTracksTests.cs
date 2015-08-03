using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Releases
{
	[TestFixture]
	public class ReleaseTracksTests
	{
		private readonly IApi _api = new ApiConnection();
		
		[Test]
		public async Task Can_hit_endpoint()
		{
			var request = _api.Create<ReleaseTracks>()
				.ForReleaseId(1996067);
			var releaseTracks = await request.Please();

			Assert.That(releaseTracks, Is.Not.Null);
			Assert.That(releaseTracks.Tracks.Count, Is.EqualTo(16));

			var track = releaseTracks.Tracks.First();
			Assert.That(track.Title, Is.EqualTo("Never Gonna Give You Up"));
		}

		[Test]
		public async Task Track_has_download_data()
		{
			var modernTimes = new DateTime(1990, 1, 1);

			var request = _api.Create<ReleaseTracks>()
				.ForReleaseId(1996067);
			var releaseTracks = await request.Please();
			var track = releaseTracks.Tracks.FirstOrDefault();

			Assert.That(track, Is.Not.Null);
			Assert.That(track.Download, Is.Not.Null);
			Assert.That(track.Download.ReleaseDate, Is.GreaterThan(modernTimes));

			Assert.That(track.Download.Packages, Is.Not.Null);
			Assert.That(track.Download.Packages.Count, Is.GreaterThan(0));

			var primaryPackage = track.Download.PrimaryPackage();

			Assert.That(primaryPackage.Id, Is.EqualTo(2));
			Assert.That(primaryPackage.Description, Is.EqualTo("standard"));
		}

		[Test]
		public async Task Track_has_download_package_price()
		{
			var request = _api.Create<ReleaseTracks>()
				.ForReleaseId(1996067);
			var releaseTracks = await request.Please();
			var track = releaseTracks.Tracks.First();
			var primaryPackage = track.Download.PrimaryPackage();

			Assert.That(primaryPackage.Price.CurrencyCode, Is.EqualTo("GBP"));
			Assert.That(primaryPackage.Price.SevendigitalPrice, Is.EqualTo(0.99));
			Assert.That(primaryPackage.Price.RecommendedRetailPrice, Is.EqualTo(0.99));
		}

		[Test]
		public async Task Track_has_download_package_formats()
		{
			var request = _api.Create<ReleaseTracks>()
				.ForReleaseId(1996067);
			var releaseTracks = await request.Please();
			var track = releaseTracks.Tracks.First();
			var primaryPackage = track.Download.PrimaryPackage();

			Assert.That(primaryPackage.Formats.Count, Is.GreaterThan(0));
			Assert.That(primaryPackage.Formats[0].Id, Is.EqualTo((17)));
			Assert.That(primaryPackage.Formats[0].Description, Is.EqualTo("MP3 320"));
		}

		[Test]
		public async Task can_determine_if_a_track_is_free()
		{
			var request = _api.Create<ReleaseTracks>()
				.ForReleaseId(394123);
			var releaseTracks = await request.Please();

			Assert.That(releaseTracks, Is.Not.Null);
			Assert.That(releaseTracks.Tracks.Count, Is.EqualTo(1));

			var track = releaseTracks.Tracks.First();
			Assert.That(track, Is.Not.Null);
			Assert.That(track.Download, Is.Not.Null);

			var price = track.Download.PrimaryPackage().Price;

			Assert.That(price, Is.Not.Null);
			Assert.That(price.SevendigitalPrice, Is.EqualTo(0m));
		}

		[Test]
		public async Task can_determine_if_a_track_is_available_separately()
		{
			var request = _api.Create<ReleaseTracks>()
				.ForReleaseId(1193196);
			var releaseTracks = await request.Please();

			Assert.That(releaseTracks, Is.Not.Null);
			Assert.That(releaseTracks.Tracks.Count, Is.GreaterThanOrEqualTo(1));

			var track = releaseTracks.Tracks.First();
			Assert.That(track, Is.Not.Null);
			Assert.That(track.Download, Is.Not.Null);

			var price = track.Download.PrimaryPackage().Price;
			Assert.That(price, Is.Not.Null);
			Assert.That(price.SevendigitalPrice.HasValue, Is.False);
		}

		[Test]
		public async Task Track_has_subscription_streaming_when_requested()
		{
			var request = _api.Create<ReleaseTracks>()
				.ForReleaseId(394123)
				.WithParameter("usageTypes", "subscriptionStreaming");
			var releaseTracks = await request.Please();

			Assert.That(releaseTracks.Tracks.Count, Is.GreaterThanOrEqualTo(1));
			var subscriptionStreaming = releaseTracks.Tracks.Select(t => t.SubscriptionStreaming);
			Assert.That(subscriptionStreaming, Is.All.Not.Null);
		}

		[Test]
		public async Task Track_has_download_when_requested()
		{
			var request = _api.Create<ReleaseTracks>()
				.ForReleaseId(1996067)
				.WithParameter("usageTypes", "download");
			var releaseTracks = await request.Please();

			Assert.That(releaseTracks.Tracks.Count, Is.GreaterThanOrEqualTo(1));
			var download = releaseTracks.Tracks.Select(t => t.Download);
			Assert.That(download, Is.All.Not.Null);
		}
	}
}