using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Tags;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Tags
{
	[TestFixture]
	public class ArtistByTagTopTests
	{
		private const string Tags = "rock,pop";

		private IApi _api;

		[TestFixtureSetUp]
		public void Setup()
		{
			_api = new ApiConnection();
		}
		
		[Test]
		public async Task Can_hit_endpoint()
		{
			var request = _api.Create<ArtistByTagTop>()
				.WithParameter("tags", Tags);
			var tags = await request.Please();

			Assert.That(tags, Is.Not.Null);
			Assert.That(tags.TaggedArtists.Count, Is.GreaterThan(0));
			Assert.That(tags.Type, Is.EqualTo(ItemType.artist));
			Assert.That(tags.TaggedArtists.FirstOrDefault().Artist.Name, Is.Not.Empty);
		}

		[Test]
		public async Task Can_hit_endpoint_with_paging()
		{
			var request = _api.Create<ArtistByTagTop>()
				.WithParameter("tags", Tags)
				.WithParameter("page", "2")
				.WithParameter("pageSize", "20");
			var artistBrowse = await request.Please();

			Assert.That(artistBrowse, Is.Not.Null);
			Assert.That(artistBrowse.Page, Is.EqualTo(2));
			Assert.That(artistBrowse.PageSize, Is.EqualTo(20));
		}
	}
}