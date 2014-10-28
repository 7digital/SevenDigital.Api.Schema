using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Pricing;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Releases
{
	[TestFixture]
	public class ReleaseTracksTests
	{
		[Test]
		public async Task Can_hit_endpoint()
		{
			var request = Api<ReleaseTracks>.Create
				.ForReleaseId(1996067);
			var releaseTracks = await request.Please();

			Assert.That(releaseTracks, Is.Not.Null);
			Assert.That(releaseTracks.Tracks.Count, Is.EqualTo(16));
			Assert.That(releaseTracks.Tracks.First().Title, Is.EqualTo("Never Gonna Give You Up"));
			Assert.That(releaseTracks.Tracks.First().Price.Status, Is.EqualTo(PriceStatus.Available));

			Assert.That(releaseTracks.Tracks.First().Download.Packages[0].Id, Is.EqualTo(2));
			Assert.That(releaseTracks.Tracks.First().Download.Packages[0].Description, Is.EqualTo("standard"));
			Assert.That(releaseTracks.Tracks.First().Download.Packages[0].Price.CurrencyCode, Is.EqualTo("GBP"));
			Assert.That(releaseTracks.Tracks.First().Download.Packages[0].Price.SevendigitalPrice, Is.EqualTo(0.99));
			Assert.That(releaseTracks.Tracks.First().Download.Packages[0].Price.RecommendedRetailPrice, Is.EqualTo(0.99));
			Assert.That(releaseTracks.Tracks.First().Download.Packages[0].Formats[0].Id, Is.EqualTo((17)));
			Assert.That(releaseTracks.Tracks.First().Download.Packages[0].Formats[0].Description, Is.EqualTo("MP3 320"));
		}

		[Test]
		public async Task can_determine_if_a_track_is_free()
		{
			var request = Api<ReleaseTracks>.Create
				.ForReleaseId(394123);
			var releaseTracks = await request.Please();

			Assert.That(releaseTracks, Is.Not.Null);
			Assert.That(releaseTracks.Tracks.Count, Is.EqualTo(1));
			Assert.That(releaseTracks.Tracks.First().Price.Status, Is.EqualTo(PriceStatus.Free));
		}

		[Test]
		public async Task can_determine_if_a_track_is_available_separately()
		{
			var request = Api<ReleaseTracks>.Create
				.ForReleaseId(1193196);
			var releaseTracks = await request.Please();

			Assert.That(releaseTracks, Is.Not.Null);
			Assert.That(releaseTracks.Tracks.Count, Is.GreaterThanOrEqualTo(1));
			Assert.That(releaseTracks.Tracks.First().Price.Status, Is.EqualTo(PriceStatus.UnAvailable));
		}
	}
}