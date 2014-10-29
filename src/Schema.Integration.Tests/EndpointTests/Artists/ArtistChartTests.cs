using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Artists;
using SevenDigital.Api.Schema.Charts;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Artists
{
	[TestFixture]
	public class ArtistChartTests
	{
		private IApi _api;

		[TestFixtureSetUp]
		public void Setup()
		{
			_api = new ApiConnection();
		}
		
		[Test]
		public async Task Can_hit_fluent_endpoint()
		{
			var chartDate = DateTime.Today.AddDays(-7);

			var request = _api.Create<ArtistChart>()
				.WithToDate(chartDate)
				.WithPeriod(ChartPeriod.Week)
				.WithPageSize(20);

			var artistChart = await request.Please();

			Assert.That(artistChart, Is.Not.Null);
			Assert.That(artistChart.ChartItems, Is.Not.Null);
			Assert.That(artistChart.ChartItems.Count, Is.EqualTo(20));

			Assert.That(artistChart.Type, Is.EqualTo(ChartType.artist));
			Assert.That(artistChart.FromDate, Is.LessThanOrEqualTo(chartDate));
			Assert.That(artistChart.ToDate, Is.GreaterThanOrEqualTo(chartDate));
			Assert.That(artistChart.ChartItems.FirstOrDefault().Artist, Is.Not.Null);
		}
	}
}