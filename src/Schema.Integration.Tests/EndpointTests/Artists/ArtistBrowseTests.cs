using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Artists;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Artists
{
	[TestFixture]
	public class ArtistBrowseTests
	{
		private readonly IApi _api = new ApiConnection();

		[Test]
		public async Task Can_hit_endpoint_with_fluent_interface()
		{
			var request = _api.Create<ArtistBrowse>()
				.WithLetter("radio");
			var artistBrowse = await request.Please();
			
			Assert.That(artistBrowse, Is.Not.Null);
			Assert.That(artistBrowse.Page, Is.EqualTo(1));
			Assert.That(artistBrowse.PageSize, Is.EqualTo(10));
			Assert.That(artistBrowse.Artists.Count, Is.GreaterThan(0));
		}

		[Test]
		public async Task Can_hit_endpoint_with_fluent_interface_with_paging()
		{
			var request = _api.Create<ArtistBrowse>()
				.WithLetter("radio")
				.WithPageNumber(2)
				.WithPageSize(20);
			var artistBrowse = await request.Please();

			Assert.That(artistBrowse, Is.Not.Null);
			Assert.That(artistBrowse.Page, Is.EqualTo(2));
			Assert.That(artistBrowse.PageSize, Is.EqualTo(20));
		}
	}
}