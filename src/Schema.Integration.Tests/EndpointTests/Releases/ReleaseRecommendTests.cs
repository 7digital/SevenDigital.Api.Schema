using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Releases
{
	[TestFixture]
	public class ReleaseRecommendTests
	{
		[Test]
		public async Task Can_hit_endpoint()
		{
			var request = Api<ReleaseRecommend>.Create
				.ForReleaseId(155408)
				.WithParameter("country", "GB");
			var release = await request.Please();

			Assert.That(release, Is.Not.Null);
			Assert.That(release.RecommendedItems.Count, Is.GreaterThan(0));
		}
	}
}