using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Releases
{
	[TestFixture]
	public class ReleaseRecommendTests
	{
		private readonly IApi _api = new ApiConnection();
		
		[Test]
		public async Task Can_hit_endpoint()
		{
			var request = _api.Create<ReleaseRecommend>()
				.ForReleaseId(155408)
				.WithParameter("country", "GB")
                .ForUsageTypes(UsageType.Download);
			var release = await request.Please();

			Assert.That(release, Is.Not.Null);
			Assert.That(release.RecommendedItems.Count, Is.GreaterThan(0));
		}
	}
}