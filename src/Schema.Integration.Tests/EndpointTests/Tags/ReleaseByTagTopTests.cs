using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Tags;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Tags
{
	[TestFixture]
	[Category("Integration")]
	public class ReleaseByTagTopTests
	{
		private readonly IApi _api = new ApiConnection();

		[Test]
		public async Task Can_hit_endpoint()
		{
			var request = _api.Create<ReleaseByTagTop>()
				.WithParameter("tags", "rock")
                .ForUsageTypes(UsageType.Download);
			var tags = await request.Please();

			Assert.That(tags, Is.Not.Null);
			Assert.That(tags.TaggedReleases.Count, Is.GreaterThan(0));
			Assert.That(tags.Type, Is.EqualTo(ItemType.release));
			Assert.That(tags.TaggedReleases.FirstOrDefault().Release.Title, Is.Not.Empty);
		}

		[Test]
		public async Task Can_hit_endpoint_with_paging()
		{
			var request = _api.Create<ReleaseByTagTop>()
				.WithParameter("tags", "rock")
				.WithParameter("page", "2")
				.WithParameter("pageSize", "20")
                .ForUsageTypes(UsageType.Download);
			var releaseByTag = await request.Please();

			Assert.That(releaseByTag, Is.Not.Null);
			Assert.That(releaseByTag.Page, Is.EqualTo(2));
			Assert.That(releaseByTag.PageSize, Is.EqualTo(20));
		}
	}
}