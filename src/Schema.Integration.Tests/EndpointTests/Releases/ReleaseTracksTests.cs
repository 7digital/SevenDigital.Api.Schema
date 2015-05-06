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

			Assert.That(track.Download.Packages[0].Id, Is.EqualTo(2));
			Assert.That(track.Download.Packages[0].Description, Is.EqualTo("standard"));
			Assert.That(track.Download.Packages[0].Price.CurrencyCode, Is.EqualTo("GBP"));
			Assert.That(track.Download.Packages[0].Price.SevendigitalPrice, Is.EqualTo(0.99));
			Assert.That(track.Download.Packages[0].Price.RecommendedRetailPrice, Is.EqualTo(0.99));
			Assert.That(track.Download.Packages[0].Formats[0].Id, Is.EqualTo((17)));
			Assert.That(track.Download.Packages[0].Formats[0].Description, Is.EqualTo("MP3 320"));
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