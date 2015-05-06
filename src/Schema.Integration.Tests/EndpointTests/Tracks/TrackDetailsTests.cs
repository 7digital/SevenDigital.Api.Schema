﻿using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Tracks;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Tracks
{
	[TestFixture]
	public class TrackDetailsTests
	{
		private IApi _api;

		[TestFixtureSetUp]
		public void Setup()
		{
			_api = new ApiConnection();
		}
		
		[Test]
		public async Task Can_hit_track_endpoint()
		{
			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);
			Assert.That(track.Title, Is.EqualTo("I Love You"));
			Assert.That(track.TrackNumber, Is.EqualTo(5));
			Assert.That(track.Number, Is.EqualTo(5));
			Assert.That(track.DiscNumber, Is.EqualTo(1));
		}

		[Test]
		public async Task Track_has_artist()
		{
			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);
			Assert.That(track.Artist, Is.Not.Null);
			Assert.That(track.Artist.Id, Is.EqualTo(437));
			Assert.That(track.Artist.Name, Is.EqualTo("The Dandy Warhols"));
			Assert.That(track.Artist.AppearsAs, Is.EqualTo("The Dandy Warhols"));
		}

		[Test]
		public async Task Track_has_release()
		{
			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);
			Assert.That(track.Release, Is.Not.Null);
			Assert.That(track.Release.Id, Is.GreaterThan(0));
			Assert.That(track.Release.Title, Is.Not.Empty);
		}

		[Test]
		public async Task Track_has_release_artist()
		{
			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);

			Assert.That(track.Release.Artist, Is.Not.Null);
			Assert.That(track.Release.Artist.Name, Is.EqualTo("The Dandy Warhols"));
			Assert.That(track.Release.Artist.AppearsAs, Is.EqualTo("The Dandy Warhols"));
		}

		[Test]
		public async Task Track_has_release_label_and_licensor()
		{
			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);

			Assert.That(track.Release.Label, Is.Not.Null);
			Assert.That(track.Release.Label.Id, Is.GreaterThan(0));
			Assert.That(track.Release.Label.Name, Is.Not.Empty);

			Assert.That(track.Release.Licensor, Is.Not.Null);
			Assert.That(track.Release.Licensor.Id, Is.GreaterThan(0));
			Assert.That(track.Release.Licensor.Name, Is.Not.Empty);
		}

		[Test]
		public async Task Track_has_packages()
		{
			var track = await GetTestTrack();

			Assert.That(track, Is.Not.Null);
			Assert.That(track.Download, Is.Not.Null);
			Assert.That(track.Download.Packages, Is.Not.Null);
			Assert.That(track.Download.Packages.Count, Is.GreaterThan(0));

			Assert.That(track.Download.Packages[0].Id, Is.EqualTo(2));
			Assert.That(track.Download.Packages[0].Description, Is.EqualTo("standard"));
			Assert.That(track.Download.Packages[0].Price.CurrencyCode, Is.EqualTo("GBP"));
			Assert.That(track.Download.Packages[0].Price.SevendigitalPrice, Is.EqualTo(0.99));
			Assert.That(track.Download.Packages[0].Price.RecommendedRetailPrice, Is.EqualTo(0.99));
			Assert.That(track.Download.Packages[0].Formats[0].Id, Is.EqualTo((17)));
			Assert.That(track.Download.Packages[0].Formats[0].Description, Is.EqualTo("MP3 320"));
		}

		private async Task<Track> GetTestTrack()
		{
			var request = _api.Create<Track>()
				.ForTrackId(12345);
			return await request.Please();
		}

	}
}