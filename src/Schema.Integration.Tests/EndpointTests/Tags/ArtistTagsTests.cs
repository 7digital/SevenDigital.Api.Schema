using System.Linq;
using System.Threading.Tasks;
using SevenDigital.Api.Wrapper;
using NUnit.Framework;
using SevenDigital.Api.Schema.Tags;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Tags
{
	[TestFixture]
	public class ArtistTagsTests
	{
		[Test]
		public async Task Can_hit_endpoint()
		{
			var request = Api<ArtistTags>.Create
				.WithParameter("artistId", "1");
			var artistTags = await request.Please();

			Assert.That(artistTags, Is.Not.Null);
			Assert.That(artistTags.TagList.Count, Is.GreaterThan(0));
			Assert.That(artistTags.TagList.FirstOrDefault().Id, Is.Not.Empty);
			Assert.That(artistTags.TagList.Where(x => x.Id == "rock").FirstOrDefault().Text, Is.EqualTo("rock"));
		}

		[Test]
		public async Task Can_hit_endpoint_with_paging()
		{
			var request = Api<ArtistTags>.Create
				.WithParameter("artistId", "2")
				.WithParameter("page", "2")
				.WithParameter("pageSize", "1");
			var artistTags = await request.Please();

			Assert.That(artistTags, Is.Not.Null);
			Assert.That(artistTags.Page, Is.EqualTo(2));
			Assert.That(artistTags.PageSize, Is.EqualTo(1));
		}
	}
}