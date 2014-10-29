using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Artists;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Artists
{
	[TestFixture]
	[Category("Integration")]
	public class ArtistTopTracksTests
	{
		private IApi _api;

		[TestFixtureSetUp]
		public void Setup()
		{
			_api = new ApiConnection();
		}


		[Test]
		public async Task Can_hit_endpoint()
		{

			var artistTopTracks = await _api.Create<ArtistTopTracks>()
				.WithParameter("artistId", "1")
				.WithParameter("country", "GB")
				.Please();

			Assert.That(artistTopTracks, Is.Not.Null);
			Assert.That(artistTopTracks.Tracks.Count, Is.GreaterThan(0));
		}

		[Test]
		public async Task Can_hit_endpoint_with_static_fluent_interface()
		{
			var artistTopTracks = await _api.Create<ArtistTopTracks>()
				.WithArtistId(1)
				.WithParameter("country", "GB")
				.Please();

			Assert.That(artistTopTracks, Is.Not.Null);
			Assert.That(artistTopTracks.Tracks.Count, Is.GreaterThan(0));
		}

		[Test]
		public async Task Can_handle_out_of_range_request()
		{
			var artistTopTracks = await _api.Create<ArtistTopTracks>()
				.WithParameter("artistId", "1")
				.WithParameter("page", "100")
				.WithParameter("pageSize", "10")
				.Please();

			Assert.That(artistTopTracks, Is.Not.Null);
			Assert.That(artistTopTracks.Tracks.Count, Is.EqualTo(0));
		}
	}
}