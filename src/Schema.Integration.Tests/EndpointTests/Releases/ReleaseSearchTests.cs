using System.Linq;
using System.Threading.Tasks;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;
using NUnit.Framework;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Releases
{
	[TestFixture]
	public class ReleaseSearchTests
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
			var request = _api.Create<ReleaseSearch>()
				.WithParameter("q", "no surprises")
				.WithParameter("type", ReleaseType.Single.ToString())
				.WithParameter("country", "GB");
			var releaseSearch = await request.Please();

			Assert.That(releaseSearch, Is.Not.Null);
			Assert.That(releaseSearch.Results.Count, Is.GreaterThan(0));
			Assert.That(releaseSearch.Results.FirstOrDefault().Release.Type, Is.EqualTo(ReleaseType.Single));
		}

		[Test]
		public async Task Can_hit_endpoint_with_paging()
		{
			var request = _api.Create<ReleaseSearch>()
				.WithParameter("q", "no surprises")
				.WithParameter("page", "2")
				.WithParameter("pageSize", "20");
			var releaseSearch = await request.Please();

			Assert.That(releaseSearch, Is.Not.Null);
			Assert.That(releaseSearch.Page, Is.EqualTo(2));
			Assert.That(releaseSearch.PageSize, Is.EqualTo(20));
		}

		[Test]
		public async Task Can_get_multiple_results()
		{
			var request = _api.Create<ReleaseSearch>()
				.WithParameter("q", "pink")
				.WithParameter("page", "1")
				.WithParameter("pageSize", "20");
			var releaseSearch = await request.Please();

			Assert.That(releaseSearch.Results.Count, Is.GreaterThan(1));
		}
	}
}