using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Tracks;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Tracks
{
	[TestFixture]
	public class TrackDetailsTests
	{
		private readonly IApi _api = new ApiConnection();

		[Test]
		public async Task Can_hit_track_endpoint()
		{
			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);
			Assert.That(track.Title, Is.EqualTo("I Love You"));
			Assert.That(track.TrackNumber, Is.EqualTo(5));
			Assert.That(track.Number, Is.EqualTo(5));
			Assert.That(track.DiscNumber, Is.EqualTo(1));
		}

		[Test]
		public async Task Track_has_artist()
		{
			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);
			Assert.That(track.Artist, Is.Not.Null);
			Assert.That(track.Artist.Id, Is.EqualTo(437));
			Assert.That(track.Artist.Name, Is.EqualTo("The Dandy Warhols"));
			Assert.That(track.Artist.AppearsAs, Is.EqualTo("The Dandy Warhols"));
			Assert.That(track.Artist.IsPlaceholderImage, Is.False);
		}

		[Test]
		public async Task Track_has_release()
		{
			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);
			Assert.That(track.Release, Is.Not.Null);
			Assert.That(track.Release.Id, Is.GreaterThan(0));
			Assert.That(track.Release.Title, Is.Not.Empty);
		}

		[Test]
		public async Task Track_has_release_artist()
		{
			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);

			Assert.That(track.Release.Artist, Is.Not.Null);
			Assert.That(track.Release.Artist.Name, Is.EqualTo("The Dandy Warhols"));
			Assert.That(track.Release.Artist.AppearsAs, Is.EqualTo("The Dandy Warhols"));
		}

		[Test]
		public async Task Track_has_release_label_and_licensor()
		{
			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);

			Assert.That(track.Release.Label, Is.Not.Null);
			Assert.That(track.Release.Label.Id, Is.GreaterThan(0));
			Assert.That(track.Release.Label.Name, Is.Not.Empty);

			Assert.That(track.Release.Licensor, Is.Not.Null);
			Assert.That(track.Release.Licensor.Id, Is.GreaterThan(0));
			Assert.That(track.Release.Licensor.Name, Is.Not.Empty);
		}

		[Test]
		public async Task Track_has_download_data()
		{
			var modernTimes = new DateTime(1990, 1, 1);

			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);
			Assert.That(track.Download, Is.Not.Null);
			Assert.That(track.Download.ReleaseDate, Is.GreaterThan(modernTimes));

			Assert.That(track.Download.Packages, Is.Not.Null);
			Assert.That(track.Download.Packages.Count, Is.GreaterThan(0));

			var primaryPackage = track.Download.PrimaryPackage();

			Assert.That(primaryPackage, Is.Not.Null);
			Assert.That(primaryPackage.Id, Is.EqualTo(2));
			Assert.That(primaryPackage.Description, Is.EqualTo("Standard"));
		}

		[Test]
		public async Task Track_has_download_price()
		{
			var track = await GetTestTrack();
			var primaryPackage = track.Download.PrimaryPackage();

			Assert.That(primaryPackage.Price.CurrencyCode, Is.EqualTo("GBP"));
			Assert.That(primaryPackage.Price.SevendigitalPrice, Is.EqualTo(0.99));
			Assert.That(primaryPackage.Price.RecommendedRetailPrice, Is.EqualTo(0.99));
		}

		[Test]
		public async Task Track_has_download_formats()
		{
			var track = await GetTestTrack();
			var primaryPackage = track.Download.PrimaryPackage();

			Assert.That(primaryPackage.Formats[0].Id, Is.EqualTo((17)));
			Assert.That(primaryPackage.Formats[0].Description, Is.EqualTo("MP3 320"));
		}

		[Test]
		public async Task Track_has_subscription_streaming_when_requested()
		{
			var request = _api.Create<Track>()
				.ForTrackId(12345)
				.ForUsageTypes(UsageType.SubscriptionStreaming);
			var track = await request.Please();
			Assert.That(track.SubscriptionStreaming, Is.Not.Null);
			Assert.That(track.SubscriptionStreaming.ReleaseDate, Is.Not.EqualTo(default(DateTime)));
		}

		[Test]
		public async Task Track_has_ad_supported_streaming_when_requested()
		{
			var request = _api.Create<Track>()
							  .ForTrackId(125996)
							  .ForUsageTypes(UsageType.AdSupportedStreaming);
			var track = await request.Please();
			Assert.That(track.AdSupportedStreaming, Is.Not.Null);
			Assert.That(track.AdSupportedStreaming.ReleaseDate, Is.Not.EqualTo(default(DateTime)));
		}

		[Test]
		public async Task Track_has_download_when_requested()
		{
			var request = _api.Create<Track>()
				.ForTrackId(12345)
				.ForUsageTypes(UsageType.Download);
			var track = await request.Please();
			Assert.That(track.Download, Is.Not.Null);
			Assert.That(track.Download.ReleaseDate, Is.Not.EqualTo(default(DateTime)));
			Assert.That(track.Download.PreviewDate, Is.Not.EqualTo(default(DateTime)));
			Assert.That(track.Download.Packages, Is.Not.Empty);
		}

		[Test]
		public async Task Track_has_slug_when_usage_types_are_requested()
		{
			var request = _api.Create<Track>()
				.ForTrackId(12345)
				.ForUsageTypes(UsageType.SubscriptionStreaming, UsageType.Download);
			var track = await request.Please();

			Assert.That(track.Artist.Slug, Is.Not.Null);
			Assert.That(track.Release.Slug, Is.Not.Null);
			Assert.That(track.Release.Artist.Slug, Is.Not.Null);
		}

		[Test]
		public async Task Track_has_pline()
		{
			var request = _api.Create<Track>()
				.ForTrackId(12345)
				.ForUsageTypes(UsageType.SubscriptionStreaming, UsageType.Download);
			var track = await request.Please();

			Assert.That(track.Pline, Is.Not.Null);
		}

		private async Task<Track> GetTestTrack()
		{
			var request = _api.Create<Track>()
				.ForTrackId(12345)
                .ForUsageTypes(UsageType.Download);
			return await request.Please();
		}
	}
}