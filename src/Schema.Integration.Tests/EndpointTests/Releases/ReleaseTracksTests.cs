using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Pricing;
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
			Assert.That(track.Price, Is.Not.Null);
			Assert.That(track.Price.Status, Is.EqualTo(PriceStatus.Available));
		}

		[Test]
		public async Task Track_has_download_data()
		{
			var modernTimes = new DateTime(1990, 1, 1);

			var request = _api.Create<ReleaseTracks>()
				.ForReleaseId(1996067);
			var releaseTracks = await request.Please();
			var track = releaseTracks.Tracks.First();

			Assert.That(track.Download, Is.Not.Null);
			Assert.That(track.Download.Packages, Is.Not.Null);
			Assert.That(track.Download.Packages.Count, Is.GreaterThan(0));

			Assert.That(track.Download.ReleaseDate.HasValue, Is.True);
			Assert.That(track.Download.ReleaseDate.Value, Is.GreaterThan(modernTimes));
		}

		[Test]
		public async Task Track_has_download_package()
		{
			var request = _api.Create<ReleaseTracks>()
				.ForReleaseId(1996067);
			var releaseTracks = await request.Please();

			var track = releaseTracks.Tracks.First();

			var primaryPackage = track.Download.PrimaryPackage();

			Assert.That(primaryPackage.Id, Is.EqualTo(2));
			Assert.That(primaryPackage.Description, Is.EqualTo("standard"));
			Assert.That(primaryPackage.Price.CurrencyCode, Is.EqualTo("GBP"));
			Assert.That(primaryPackage.Price.SevendigitalPrice, Is.EqualTo(0.99));
			Assert.That(primaryPackage.Price.RecommendedRetailPrice, Is.EqualTo(0.99));
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
			Assert.That(track.Price, Is.Not.Null);
			Assert.That(track.Price.Status, Is.EqualTo(PriceStatus.Free));
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
			Assert.That(track.Price, Is.Not.Null);
			Assert.That(track.Price.Status, Is.EqualTo(PriceStatus.UnAvailable));
		}
	}
}