using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Artists;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Artists
{
	[TestFixture]
	public class ArtistReleasesTests
	{
		private readonly IApi _api = new ApiConnection();

		[Test]
		public async Task Can_hit_endpoint_with_fluent_interface()
		{
			var request = _api.Create<ArtistReleases>()
				.WithArtistId(1);
			var artistReleases = await request.Please();

			Assert.That(artistReleases, Is.Not.Null);
			Assert.That(artistReleases.Releases.Count, Is.GreaterThan(0));
			Assert.That(artistReleases.Releases.FirstOrDefault().Artist.Name, Is.EqualTo("Keane"));
		}

		[Test]
		public async Task Can_hit_endpoint_with_paging()
		{
			var request = _api.Create<ArtistReleases>()
				.WithPageNumber(2)
				.WithPageSize(20)
				.WithParameter("artistId", "1");
			var artistBrowse = await request.Please();
			
			Assert.That(artistBrowse, Is.Not.Null);
			Assert.That(artistBrowse.Page, Is.EqualTo(2));
			Assert.That(artistBrowse.PageSize, Is.EqualTo(20));
		}
	}
}