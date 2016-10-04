using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Charts;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Schema.Releases;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Releases
{
	[TestFixture]
	[Category("Integration")]
	public class ReleaseChartTests
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
			var request = _api.Create<ReleaseChart>()
				.WithParameter("fromDate", "20110101")
				.WithParameter("toDate", "20110301")
				.WithParameter("country", "GB");
			var releaseChart = await request.Please();

			Assert.That(releaseChart, Is.Not.Null);
			Assert.That(releaseChart.ChartItems.Count, Is.GreaterThan(0));
			Assert.That(releaseChart.Type, Is.EqualTo(ChartType.album));
		}

		[Test]
		public async Task Can_hit_endpoint_with_paging()
		{
			var request = _api.Create<ReleaseChart>()
				.WithParameter("fromDate", "20090610")
				.WithParameter("toDate", "20110101")
				.WithParameter("page", "2")
				.WithParameter("pageSize", "20");
			var releaseChart = await request.Please();

			Assert.That(releaseChart, Is.Not.Null);
			Assert.That(releaseChart.Page, Is.EqualTo(2));
			Assert.That(releaseChart.PageSize, Is.EqualTo(20));
		}

		[Test]
		public async Task Can_hit_fluent_endpoint()
		{
			var request = _api.Create<ReleaseChart>()
				.WithToDate(new DateTime(2011, 01, 31))
				.WithPeriod(ChartPeriod.Week);
			var releaseChart = await request.Please();

			Assert.That(releaseChart, Is.Not.Null);
			Assert.That(releaseChart.ChartItems.Count, Is.EqualTo(10));
			Assert.That(releaseChart.Type, Is.EqualTo(ChartType.album));
			Assert.That(releaseChart.ChartItems.FirstOrDefault().Release, Is.Not.Null);
		}
	}
}