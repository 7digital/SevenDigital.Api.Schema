using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Artists;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Artists
{
	[TestFixture]
	[Category("Integration")]
	public class ArtistSearchTests
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
			var request = _api.Create<ArtistSearch>()
				.WithParameter("q", "pink")
				.WithParameter("country", "GB");
			var artist = await request.Please();

			Assert.That(artist, Is.Not.Null);
		}
		
		[Test]
		public async Task Can_get_multiple_results()
		{
			var request = _api.Create<ArtistSearch>()
				.ForShop(34)
				.WithQuery("pink")
				.WithPageNumber(1)
				.WithPageSize(20);
			var artistSearch = await request.Please();

			Assert.That(artistSearch.Results.Count, Is.GreaterThan(1));
		}

		[Test]
		public async Task Can_hit_endpoint_with_paging()
		{
			var request = _api.Create<ArtistSearch>()
				.WithParameter("q", "pink")
				.WithParameter("page", "2")
				.WithParameter("pageSize", "20");
			var artistBrowse = await request.Please();

			Assert.That(artistBrowse, Is.Not.Null);
			Assert.That(artistBrowse.Page, Is.EqualTo(2));
			Assert.That(artistBrowse.PageSize, Is.EqualTo(20));
		}

		[Test]
		public async Task Can_hit_endpoint_with_static_interface()
		{
			var request = Api<ArtistSearch>
				.Create
				.WithQuery("pink")
				.WithParameter("country", "GB");
			var artistSearch = await request.Please();

			Assert.That(artistSearch, Is.Not.Null);
		}

		[Test]
		public async Task Can_get_multiple_results_with_static_interface()
		{
			var request = Api<ArtistSearch>.Create
				.WithParameter("q", "pink")
				.WithParameter("page", "1")
				.WithParameter("pageSize", "20");
			var artistSearch = await request.Please();

			Assert.That(artistSearch.Results.Count, Is.GreaterThan(1));
		}
	}
}