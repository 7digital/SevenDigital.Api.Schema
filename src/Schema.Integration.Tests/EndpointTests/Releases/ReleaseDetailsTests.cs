﻿using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Schema.Releases;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Releases
{
	[TestFixture]
	[Category("Integration")]
	public class ReleaseDetailsTests
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
			var release = await GetTestRelease();

			Assert.That(release, Is.Not.Null);
			Assert.That(release.Title, Is.EqualTo("Strangeland"));
			Assert.That(release.Artist.Name, Is.EqualTo("Keane"));
			Assert.That(release.TrackCount, Is.EqualTo(12));
			Assert.That(release.StreamingReleaseDate, Is.EqualTo(DateTime.Parse("2012-05-07")));
			Assert.That(release.Duration, Is.EqualTo(2716));
			Assert.That(release.Price.Currency.Symbol, Is.EqualTo("£"));
		}

		[Test]
		public async Task Release_has_formats()
		{
			var release = await GetTestRelease();

			Assert.That(release, Is.Not.Null);
			Assert.That(release.Formats, Is.Not.Null);
			Assert.That(release.Formats.Formats, Is.Not.Null);
			Assert.That(release.Formats.Formats.Count, Is.GreaterThan(0));
			Assert.That(release.Formats.Formats[0].FileFormat, Is.EqualTo("MP3"));
		}

		[Test]
		public async Task Release_has_artist()
		{
			var release = await GetTestRelease();

			Assert.That(release, Is.Not.Null);
			Assert.That(release.Artist, Is.Not.Null);
			Assert.That(release.Artist.Id, Is.GreaterThan(0));
		}

		[Test]
		public async Task Release_has_packages()
		{
			var release = await GetTestRelease();

			Assert.That(release, Is.Not.Null);
			Assert.That(release.Download, Is.Not.Null);
			Assert.That(release.Download.Packages, Is.Not.Null);
			Assert.That(release.Download.Packages.Count, Is.GreaterThan(0));

			Assert.That(release.Download.Packages[0].Id, Is.EqualTo(2));
			Assert.That(release.Download.Packages[0].Description, Is.EqualTo("standard"));
			Assert.That(release.Download.Packages[0].Price.CurrencyCode, Is.EqualTo("GBP"));
			Assert.That(release.Download.Packages[0].Price.SevendigitalPrice, Is.EqualTo(8.99));
			Assert.That(release.Download.Packages[0].Price.RecommendedRetailPrice, Is.EqualTo(8.99));
			Assert.That(release.Download.Packages[0].Formats[0].Id, Is.EqualTo((17)));
			Assert.That(release.Download.Packages[0].Formats[0].Description, Is.EqualTo("MP3 320"));
		}

		private async Task<Release> GetTestRelease()
		{
			var request = _api.Create<Release>()
				.ForReleaseId(1685647)
				.WithParameter("country", "GB");

			return await request.Please();
		}

	}
}