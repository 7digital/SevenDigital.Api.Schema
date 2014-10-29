using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Tracks;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Tracks
{
	[TestFixture]
	public class TrackPreviewTests
	{
		private IApi _api;

		[TestFixtureSetUp]
		public void Setup()
		{
			_api = new ApiConnection();
		}
		
		[Test]
		public async Task Can_hit_endpoint_with_redirect_false()
		{
			var request = _api.Create<TrackPreview>()
				.WithParameter("trackid", "123")
				.WithParameter("redirect", "false");
			var track = await request.Please();

			Assert.That(track, Is.Not.Null);
			Assert.That(track.Url, Is.StringStarting("http://previews.7digital.com/clip"));
		}
	}
}