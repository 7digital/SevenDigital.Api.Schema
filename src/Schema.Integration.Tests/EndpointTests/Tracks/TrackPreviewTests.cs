using NUnit.Framework;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Tracks;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Tracks
{
	[TestFixture]
	public class TrackPreviewTests
	{
		[Test]
		public async void Can_hit_endpoint_with_redirect_false()
		{
			var request = Api<TrackPreview>.Create
				.WithParameter("trackid", "123")
				.WithParameter("redirect", "false");
			var track = await request.Please();

			Assert.That(track, Is.Not.Null);
			Assert.That(track.Url, Is.StringStarting("http://previews.7digital.com/clip"));
		}
	}
}