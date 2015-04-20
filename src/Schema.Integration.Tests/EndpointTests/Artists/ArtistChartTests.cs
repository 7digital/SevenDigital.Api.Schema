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
		private readonly IApi _api = new ApiConnection();

		[Test]
		public async Task Can_use_artist_chart_endpoint()
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
		}

		[Test]
		public async Task Endpoint_returns_artist_data()
		{
			var chartDate = DateTime.Today.AddDays(-7);

			var request = _api.Create<ArtistChart>()
				.WithToDate(chartDate)
				.WithPeriod(ChartPeriod.Week)
				.WithPageSize(20);

			var artistChart = await request.Please();
			var no1 = artistChart.ChartItems.FirstOrDefault();

			Assert.That(no1, Is.Not.Null);
			Assert.That(no1.Artist, Is.Not.Null);

			Assert.That(no1.Artist.Id, Is.GreaterThan(0));
			Assert.That(no1.Artist.Name, Is.Not.Empty);
			Assert.That(no1.Artist.Url, Is.Not.Null.Or.Empty);
			Assert.That(no1.Artist.Image, Is.Not.Null.Or.Empty);
		}
	}
}