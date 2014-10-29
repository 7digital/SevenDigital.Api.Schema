using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Artists;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Artists
{
	[TestFixture]
	public class ArtistDetailsTests
	{
		private IApi _api;

		[TestFixtureSetUp]
		public void Setup()
		{
			_api = new ApiConnection();
		}
		
		[Test]
		public async Task Can_hit_endpoint_with_fluent_interface()
		{
			var request = _api.Create<Artist>()
				.WithArtistId(1);
			var artist = await request.Please();

			Assert.That(artist, Is.Not.Null);
			Assert.That(artist.Name, Is.EqualTo("Keane"));
			Assert.That(artist.SortName, Is.EqualTo("Keane"));
			Assert.That(artist.Url, Is.StringStarting("http://www.7digital.com/artist/keane/"));
			Assert.That(artist.Image, Is.EqualTo("http://artwork-cdn.7static.com/static/img/artistimages/00/000/000/0000000001_150.jpg"));
		}
	}
}