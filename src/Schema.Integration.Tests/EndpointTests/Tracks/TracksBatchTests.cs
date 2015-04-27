using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Schema.Tracks;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Tracks
{
	[TestFixture]
	public class TracksBatchTests
	{
		private IApi _api;
		private const int FirstId = 12345;
		private const int SecondId = 12346;

		[TestFixtureSetUp]
		public void Setup()
		{
			_api = new ApiConnection();
		}

		[Test]
		public async Task Should_return_tracks_from_api()
		{
			var ids = new List<int> { FirstId, SecondId };
			var request = _api.Create<TracksBatch>()
				.WithParameter("trackids", ids)
				.ForShop(34);

			var response = await request.Please();

			Assert.That(response, Is.Not.Null);
			Assert.That(response.Tracks, Is.Not.Null);

			var tracks = response.Tracks;
			Assert.That(tracks.Count, Is.EqualTo(2));
			Assert.That(tracks[0].Id, Is.EqualTo(FirstId));
			Assert.That(tracks[1].Id, Is.EqualTo(SecondId));
		}

		[Test]
		public async Task Should_return_tracks_in_same_order_as_request()
		{
			var ids = new List<int> { SecondId, FirstId };
			var request = _api.Create<TracksBatch>()
				.WithParameter("trackids", ids)
				.ForShop(34);

			var response = await request.Please();
			var tracks = response.Tracks;

			Assert.That(tracks.Count, Is.EqualTo(2));
			Assert.That(tracks[0].Id, Is.EqualTo(SecondId));
			Assert.That(tracks[1].Id, Is.EqualTo(FirstId));
		}

		[Test]
		public async Task Should_return_tracks_and_errors_when_showErrors_flag_is_specified()
		{
			var ids = new List<int> { FirstId, SecondId, -1, -2 };
			var request = _api.Create<TracksBatch>()
				.WithParameter("trackids", ids)
				.WithParameter("showErrors", "true")
				.ForShop(34);

			var response = await request.Please();

			Assert.That(response, Is.Not.Null);

			Assert.That(response.Tracks, Is.Not.Null);
			Assert.That(response.Tracks.Count, Is.EqualTo(2));

			Assert.That(response.Errors, Is.Not.Null);
			Assert.That(response.Errors.Count, Is.EqualTo(2));
			Assert.That(response.Errors[0].ItemId, Is.EqualTo(-1));
			Assert.That(response.Errors[1].ItemId, Is.EqualTo(-2));
		}

		[Test]
		public async Task Should_return_error_details()
		{
			var ids = new List<int> { -1 };
			var request = _api.Create<TracksBatch>()
				.WithParameter("trackids", ids)
				.WithParameter("showErrors", "true")
				.ForShop(34);

			var response = await request.Please();

			Assert.That(response, Is.Not.Null);

			var error = response.Errors[0];
			Assert.That(error.ItemId, Is.EqualTo(-1));
			Assert.That(error.Code, Is.EqualTo(2001));
			Assert.That(error.ErrorMessage, Is.Not.Empty);
		}

		[Test]
		public async Task Should_not_return_errors_in_by_default()
		{
			var ids = new List<int> { FirstId, SecondId, -1, -2 };
			var request = _api.Create<TracksBatch>()
				.WithParameter("trackids", ids)
				.ForShop(34);

			var response = await request.Please();

			Assert.That(response, Is.Not.Null);

			Assert.That(response.Tracks, Is.Not.Null);
			Assert.That(response.Tracks.Count, Is.EqualTo(2));

			Assert.That(response.Errors, Is.Not.Null);
			Assert.That(response.Errors.Count, Is.EqualTo(0));
		}
	}
}
