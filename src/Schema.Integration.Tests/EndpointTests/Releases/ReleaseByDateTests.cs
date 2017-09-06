using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Schema.Releases;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Releases
{
	[TestFixture]
	[Category("Integration")]
	public class ReleaseByDateTests
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
			var request = _api.Create<ReleaseByDate>()
				.WithParameter("fromDate", DateTime.Now.AddDays(-7).ToString("yyyyMMdd"))
				.WithParameter("toDate", DateTime.Now.ToString("yyyyMMdd"))
				.WithParameter("country", "GB")
                .WithParameter("usageTypes", "download");

			var release = await request.Please();

			Assert.That(release, Is.Not.Null);
			Assert.That(release.Releases.Count, Is.GreaterThan(0));
		}

		[Test]
		public async Task Can_hit_endpoint_with_paging()
		{
			var request = _api.Create<ReleaseByDate>()
				.WithParameter("fromDate", "20140901")
				.WithParameter("toDate", "20140930")
				.WithParameter("page", "2")
				.WithParameter("pageSize", "20")
                .WithParameter("usageTypes", "download");
			var releaseByDate = await request.Please();

			Assert.That(releaseByDate, Is.Not.Null);
			Assert.That(releaseByDate.Page, Is.EqualTo(2));
			Assert.That(releaseByDate.PageSize, Is.EqualTo(20));
			Assert.That(releaseByDate.Releases.Count, Is.GreaterThan(0));
		}
	}
}